using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using BilcoManagement.Repositories;

namespace BilcoManagement.Services
{
    public class LoaiVatTuService : BaseService<LoaiVatTu, LoaiVatTuDTO, CreateLoaiVatTuDTO, UpdateLoaiVatTuDTO>, ILoaiVatTuService
    {
        public LoaiVatTuService(ILoaiVatTuRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
