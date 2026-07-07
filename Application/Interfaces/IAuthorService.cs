using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto?> GetByIdAsync(int id);

        Task<IEnumerable<AuthorDto>> GetAllAsync();

        Task<int> CreateAsync(CreateAuthorDto dto);

        Task UpdateAsync(int id, UpdateAuthorDto dto);

        Task DeleteAsync(int id);
    }
}

