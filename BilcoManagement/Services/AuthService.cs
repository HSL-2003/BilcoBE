using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using BilcoManagement.Settings;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BilcoManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly QuanLyChatLuongSanPhamContext _context;

        public AuthService(
            INguoiDungRepository nguoiDungRepository,
            IMapper mapper,
            IOptions<JwtSettings> jwtOptions,
            QuanLyChatLuongSanPhamContext context)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _mapper = mapper;
            _jwtSettings = jwtOptions.Value;
            _context = context;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterNguoiDungDTO registerDto)
        {
            if (await _nguoiDungRepository.UserNameExistsAsync(registerDto.TenDangNhap))
            {
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại");
            }

            NormalizeForeignKeys(registerDto);

            await ValidateForeignKeysAsync(registerDto);

            var user = _mapper.Map<NguoiDung>(registerDto);
            user.MatKhau = HashPassword(registerDto.MatKhau);
            user.NgayTao = DateTime.UtcNow;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            // Nếu người dùng tự đăng ký (không gắn nhân viên), chuyển sang trạng thái chờ duyệt
            if (!registerDto.MaNV.HasValue)
            {
                user.TrangThai = false;
                user.IsActive = false;
                user.MaQuyen = null;
            }
            else
            {
                user.TrangThai ??= true;
                user.IsActive ??= true;
            }

            await _nguoiDungRepository.AddAsync(user);

            return GenerateAuthResponse(user);
        }

        private async Task ValidateForeignKeysAsync(RegisterNguoiDungDTO registerDto)
        {
            if (registerDto.MaNV.HasValue)
            {
                var exists = await _context.NhanViens.AnyAsync(x => x.MaNV == registerDto.MaNV.Value);
                if (!exists)
                {
                    throw new ArgumentException($"Không tồn tại nhân viên với mã {registerDto.MaNV}");
                }
            }

            if (registerDto.MaQuyen.HasValue)
            {
                var roleExists = await _context.PhanQuyens.AnyAsync(x => x.MaQuyen == registerDto.MaQuyen.Value);
                if (!roleExists)
                {
                    throw new ArgumentException($"Không tồn tại quyền với mã {registerDto.MaQuyen}");
                }
            }
        }

        private static void NormalizeForeignKeys(RegisterNguoiDungDTO registerDto)
        {
            if (registerDto.MaNV.HasValue && registerDto.MaNV.Value <= 0)
            {
                registerDto.MaNV = null;
            }

            if (registerDto.MaQuyen.HasValue && registerDto.MaQuyen.Value <= 0)
            {
                registerDto.MaQuyen = null;
            }
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDto)
        {
            var user = await _nguoiDungRepository.GetByUserNameAsync(loginDto.TenDangNhap);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng");
            }

            var passwordValid = VerifyPassword(loginDto.MatKhau, user);
            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng");
            }

            if (user.TrangThai == false || user.IsActive == false)
            {
                throw new InvalidOperationException("Tài khoản của bạn đang chờ duyệt hoặc đã bị khóa");
            }

            user.LastLogin = DateTime.UtcNow;
            await _nguoiDungRepository.UpdateAsync(user.MaND, user);

            return GenerateAuthResponse(user);
        }

        public async Task<NguoiDungDTO> ApproveUserAsync(int userId, ApproveNguoiDungDTO approveDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Lấy thông tin người dùng
                var user = await _nguoiDungRepository.GetByIdAsync(userId) 
                    ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

                // 2. Kiểm tra quyền có tồn tại không
                var roleExists = await _context.PhanQuyens.AnyAsync(pq => pq.MaQuyen == approveDto.MaQuyen);
                if (!roleExists)
                {
                    throw new ArgumentException($"Không tồn tại quyền với mã {approveDto.MaQuyen}");
                }

                // 3. Tạo hoặc cập nhật thông tin nhân viên
                NhanVien nhanVien;
                
                if (user.MaNV.HasValue)
                {
                    // Cập nhật nhân viên đã tồn tại
                    nhanVien = await _context.NhanViens.FindAsync(user.MaNV.Value);
                    if (nhanVien != null)
                    {
                        nhanVien.HoTen = approveDto.HoTen;
                        nhanVien.Email = approveDto.Email;
                        nhanVien.SoDienThoai = approveDto.SoDienThoai;
                        nhanVien.ChucVu = approveDto.ChucVu;
                        nhanVien.PhongBan = approveDto.PhongBan;
                        _context.NhanViens.Update(nhanVien);
                    }
                    else
                    {
                        // Nếu có MaNV nhưng không tìm thấy bản ghi tương ứng, tạo mới
                        nhanVien = new NhanVien
                        {
                            HoTen = approveDto.HoTen,
                            Email = approveDto.Email,
                            SoDienThoai = approveDto.SoDienThoai,
                            ChucVu = approveDto.ChucVu,
                            PhongBan = approveDto.PhongBan
                        };
                        await _context.NhanViens.AddAsync(nhanVien);
                    }
                }
                else
                {
                    // Tạo mới nhân viên
                    nhanVien = new NhanVien
                    {
                        HoTen = approveDto.HoTen,
                        Email = approveDto.Email,
                        SoDienThoai = approveDto.SoDienThoai,
                        ChucVu = approveDto.ChucVu,
                        PhongBan = approveDto.PhongBan
                    };
                    await _context.NhanViens.AddAsync(nhanVien);
                }

                await _context.SaveChangesAsync();

                // 4. Cập nhật thông tin người dùng
                user.MaNV = nhanVien.MaNV;
                user.MaQuyen = approveDto.MaQuyen;
                user.TrangThai = approveDto.TrangThai;
                user.IsActive = approveDto.IsActive;
                user.UpdatedAt = DateTime.UtcNow;

                await _nguoiDungRepository.UpdateAsync(userId, user);
                await transaction.CommitAsync();

                return _mapper.Map<NguoiDungDTO>(user);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateNhanVienInfoAsync(int maNV, UpdateNhanVienDto updateDto)
        {
            var nhanVien = await _context.NhanViens.FindAsync(maNV);
            if (nhanVien == null)
            {
                throw new KeyNotFoundException("Không tìm thấy nhân viên");
            }

            _mapper.Map(updateDto, nhanVien);
            _context.NhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<NguoiDungDTO> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Lấy thông tin người dùng
                var user = await _nguoiDungRepository.GetByIdAsync(userId) 
                    ?? throw new KeyNotFoundException("Không tìm thấy người dùng");

                // 2. Cập nhật mật khẩu nếu có
                if (!string.IsNullOrEmpty(updateDto.MatKhau))
                {
                    user.MatKhau = HashPassword(updateDto.MatKhau);
                }

                // 3. Cập nhật thông tin trong bảng NhanVien nếu có MaNV
                if (user.MaNV.HasValue)
                {
                    var nhanVien = await _context.NhanViens.FindAsync(user.MaNV.Value);
                    if (nhanVien != null)
                    {
                        if (!string.IsNullOrEmpty(updateDto.HoTen))
                        {
                            nhanVien.HoTen = updateDto.HoTen;
                        }
                        if (!string.IsNullOrEmpty(updateDto.SoDienThoai))
                        {
                            nhanVien.SoDienThoai = updateDto.SoDienThoai;
                        }
                        if (!string.IsNullOrEmpty(updateDto.Email))
                        {
                            nhanVien.Email = updateDto.Email;
                        }
                        _context.NhanViens.Update(nhanVien);
                        await _context.SaveChangesAsync();
                    }
                }

                // 4. Cập nhật thời gian sửa đổi
                user.UpdatedAt = DateTime.UtcNow;
                await _nguoiDungRepository.UpdateAsync(userId, user);
                
                await transaction.CommitAsync();

                return _mapper.Map<NguoiDungDTO>(user);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<NguoiDungDTO> GetUserByIDAsync(int userId)
        {
            var user = await _nguoiDungRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng");
            }

            return _mapper.Map<NguoiDungDTO>(user);
        }
        public async Task<NguoiDungDTO> DeleteUserByIdAsync(int userId)
        {
            var user = await _nguoiDungRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng");
            }

            await _nguoiDungRepository.DeleteAsync(userId);
            return _mapper.Map<NguoiDungDTO>(user);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, NguoiDung user)
        {
            var storedPassword = user.MatKhau ?? string.Empty;

            if (storedPassword.StartsWith("$2"))
            {
                return BCrypt.Net.BCrypt.Verify(password, storedPassword);
            }

            var isLegacyMatch = password == storedPassword;
            if (isLegacyMatch)
            {
                user.MatKhau = HashPassword(password);
            }

            return isLegacyMatch;
        }

        private AuthResponseDTO GenerateAuthResponse(NguoiDung user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.TenDangNhap),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.MaND.ToString()) // Dùng claim type riêng cho userId
            };

            if (user.MaQuyen.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.MaQuyen.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires,
                MaND = user.MaND,
                TenDangNhap = user.TenDangNhap,
                MaQuyen = user.MaQuyen
            };
        }
        public async Task<IEnumerable<NguoiDungDTO>> GetAllUsersAsync()
        {
            var users = await _nguoiDungRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NguoiDungDTO>>(users);
        }

        public async Task<IEnumerable<NguoiDungDTO>> GetPendingUsersAsync()
        {
            var pendingUsers = await _nguoiDungRepository.GetPendingUsersAsync();
            return _mapper.Map<IEnumerable<NguoiDungDTO>>(pendingUsers);
        }

        public async Task<NguoiDungDTO> AdminCreateUserAsync(AdminCreateNguoiDungDTO createDto)
        {
            if (await _nguoiDungRepository.UserNameExistsAsync(createDto.TenDangNhap))
            {
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại");
            }

            // Validate foreign keys
           

            var roleExists = await _context.PhanQuyens.AnyAsync(pq => pq.MaQuyen == createDto.MaQuyen);
            if (!roleExists)
            {
                throw new ArgumentException($"Không tồn tại quyền với mã {createDto.MaQuyen}");
            }

            var user = _mapper.Map<NguoiDung>(createDto);
            user.MatKhau = HashPassword(createDto.MatKhau);
            user.NgayTao = DateTime.UtcNow;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.MaNV = null;
            // Set default values if not provided
            user.TrangThai = createDto.TrangThai;
            user.IsActive = createDto.IsActive;

            await _nguoiDungRepository.AddAsync(user);

            return _mapper.Map<NguoiDungDTO>(user);
        }

     
    }
}
