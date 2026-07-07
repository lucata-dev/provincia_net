using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);

        Task<IEnumerable<Book>> GetAllAsync();

        Task AddAsync(Book book);

        Task<Loan?> GetLoanByIdAsync(int id);

        void Update(Book book);

        void Remove(Book book);

        Task SaveChangesAsync();
    }
}
