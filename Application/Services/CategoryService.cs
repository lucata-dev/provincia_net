using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) return;
            _repository.Remove(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c is null) return null;
            return new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) return;

            if (dto.Name is not null) category.Name = dto.Name;
            if (dto.Description is not null) category.Description = dto.Description;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }
    }
}
