using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();

        Task AddAsync(Loan book);

        void Update(Loan book);

        void Remove(Loan book);

        Task<Loan?> GetLoanByIdAsync(int id);

        Task SaveChangesAsync();
    }
}
