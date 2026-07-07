using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();

        Task AddAsync(Loan loan);

        void Update(Loan loan);

        void Remove(Loan loan);

        Task<Loan?> GetLoanByIdAsync(int id);

        Task SaveChangesAsync();
    }
}
