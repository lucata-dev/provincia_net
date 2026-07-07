using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetByIdAsync(int id);

        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<int> CreateAsync(CreateCategoryDto dto);

        Task UpdateAsync(int id, UpdateCategoryDto dto);

        Task DeleteAsync(int id);
    }
}

