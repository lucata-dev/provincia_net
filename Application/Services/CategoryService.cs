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
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.categoryRepository.GetByIdAsync(id);
            if (category is null) return;
            this.categoryRepository.Remove(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await this.categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await this.categoryRepository.GetByIdAsync(id);
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
            var category = await this.categoryRepository.GetByIdAsync(id);
            if (category is null) return;

            if (dto.Name is not null) category.Name = dto.Name;
            if (dto.Description is not null) category.Description = dto.Description;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
