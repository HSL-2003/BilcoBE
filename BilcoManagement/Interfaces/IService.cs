using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilcoManagement.Interfaces
{
    public interface IService<T, TDto, TCreateDto, TUpdateDto>
        where T : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TCreateDto createDto);
        Task UpdateAsync(int id, TUpdateDto updateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
