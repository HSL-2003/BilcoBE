using AutoMapper;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using BilcoManagement.Profiles;
using BilcoManagement.Repositories;
using BilcoManagement.Services;
using BilcoManagement.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuanLyChatLuongSanPhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add AutoMapper
builder.Services.AddAutoMapper(
    typeof(Program),
    typeof(KhoProfile),
    typeof(TonKhoProfile),
    typeof(ChiTietDieuChuyenProfile),
    typeof(ChiTietKiemKeProfile),
    typeof(ChiTietPhieuNhapProfile),
    typeof(ChiTietPhieuXuatProfile),
    typeof(ChiTietPhuTungProfile),
    typeof(DieuChuyenKhoProfile),
    typeof(DonViTinhProfile),
    typeof(HinhAnhBaoTriProfile),
    typeof(KeHoachBaoTriProfile),
    typeof(KiemKeKhoProfile),
    typeof(LichSuSuCoProfile),
    typeof(LoaiThietBiProfile),
    typeof(NguoiDungProfile),
    typeof(VatTuProfile),
    typeof(LoaiVatTuProfile),
    typeof(NhaCungCapProfile));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Add Repositories

builder.Services.AddScoped<IKhoRepository, KhoRepository>();
builder.Services.AddScoped<ITonKhoRepository, TonKhoRepository>();
builder.Services.AddScoped<IChiTietDieuChuyenRepository, ChiTietDieuChuyenRepository>();
builder.Services.AddScoped<IChiTietKiemKeRepository, ChiTietKiemKeRepository>();
builder.Services.AddScoped<IChiTietPhieuNhapRepository, ChiTietPhieuNhapRepository>();
builder.Services.AddScoped<IChiTietPhieuXuatRepository, ChiTietPhieuXuatRepository>();
builder.Services.AddScoped<IThietBiRepository, ThietBiRepository>();
builder.Services.AddScoped<IChiTietPhuTungRepository, ChiTietPhuTungRepository>();
builder.Services.AddScoped<IDieuChuyenKhoRepository, DieuChuyenKhoRepository>();
builder.Services.AddScoped<IDonViTinhRepository, DonViTinhRepository>();
builder.Services.AddScoped<IHinhAnhBaoTriRepository, HinhAnhBaoTriRepository>();
builder.Services.AddScoped<IKeHoachBaoTriRepository, KeHoachBaoTriRepository>();
builder.Services.AddScoped<IKiemKeKhoRepository, KiemKeKhoRepository>();
builder.Services.AddScoped<ILichSuSuCoRepository, LichSuSuCoRepository>();
builder.Services.AddScoped<ILoaiThietBiRepository, LoaiThietBiRepository>();
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<IVatTuRepository, VatTuRepository>();
builder.Services.AddScoped<ILoaiVatTuRepository, LoaiVatTuRepository>();
builder.Services.AddScoped<INhaCungCapRepository, NhaCungCapRepository>();
builder.Services.AddScoped<IPhanQuyenRepository, PhanQuyenRepository>();
builder.Services.AddScoped<IPhieuBaoTriRepository, PhieuBaoTriRepository>();
builder.Services.AddScoped<IPhieuNhapKhoRepository, PhieuNhapKhoRepository>();
builder.Services.AddScoped<IPhieuXuatKhoRepository, PhieuXuatKhoRepository>();

// Add Services
builder.Services.AddScoped<IKhoService, KhoService>();
builder.Services.AddScoped<ITonKhoService, TonKhoService>();
builder.Services.AddScoped<IChiTietDieuChuyenService, ChiTietDieuChuyenService>();
builder.Services.AddScoped<IChiTietKiemKeService, ChiTietKiemKeService>();
builder.Services.AddScoped<IChiTietPhieuNhapService, ChiTietPhieuNhapService>();
builder.Services.AddScoped<IChiTietPhieuXuatService, ChiTietPhieuXuatService>();
builder.Services.AddScoped<IThietBiService, ThietBiService>();
builder.Services.AddScoped<IChiTietPhuTungService, ChiTietPhuTungService>();
builder.Services.AddScoped<IDieuChuyenKhoService, DieuChuyenKhoService>();
builder.Services.AddScoped<IDonViTinhService, DonViTinhService>();
builder.Services.AddScoped<IHinhAnhBaoTriService, HinhAnhBaoTriService>();
builder.Services.AddScoped<IKeHoachBaoTriService, KeHoachBaoTriService>();
builder.Services.AddScoped<IKiemKeKhoService, KiemKeKhoService>();
builder.Services.AddScoped<ILichSuSuCoService, LichSuSuCoService>();
builder.Services.AddScoped<ILoaiThietBiService, LoaiThietBiService>();
builder.Services.AddScoped<IVatTuService, VatTuService>();
builder.Services.AddScoped<ILoaiVatTuService, LoaiVatTuService>();
builder.Services.AddScoped<INhaCungCapService, NhaCungCapService>();
builder.Services.AddScoped<IPhanQuyenService, PhanQuyenService>();
builder.Services.AddScoped<IPhieuBaoTriService, PhieuBaoTriService>();
builder.Services.AddScoped<IPhieuNhapKhoService, PhieuNhapKhoService>();
builder.Services.AddScoped<IPhieuXuatKhoService, PhieuXuatKhoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        RoleClaimType = ClaimTypes.Role
    };
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with JWT Authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bilco Management API", Version = "v1" });
    
    // Add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Nhập JWT Bearer token (Chỉ cần nhập token, không cần thêm 'Bearer ' phía trước)",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DefaultCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
