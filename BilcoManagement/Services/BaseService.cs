using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilcoManagement.Services
{
    public abstract class BaseService<T, TDto, TCreateDto, TUpdateDto> : IService<T, TDto, TCreateDto, TUpdateDto>
        where T : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<T>(createDto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(int id, TUpdateDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found");
            }

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
