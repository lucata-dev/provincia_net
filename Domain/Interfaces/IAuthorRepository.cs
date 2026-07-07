using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id);

        Task<IEnumerable<Author>> GetAllAsync();

        Task AddAsync(Author author);

        void Update(Author author);

        void Remove(Author author);

        Task SaveChangesAsync();
    }
}
