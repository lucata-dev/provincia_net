using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);

        Task<IEnumerable<Category>> GetAllAsync();

        Task AddAsync(Category category);

        void Update(Category category);

        void Remove(Category category);

        Task SaveChangesAsync();
    }
}
