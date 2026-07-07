using Application.DTOs;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto?> GetByIdAsync(int id);

        Task<IEnumerable<LoanDto>> GetAllAsync();

        Task<int> CreateAsync(CreateLoanDto dto);

        Task UpdateAsync(int id, UpdateLoanDto dto);

        Task DeleteAsync(int id);

        Task ReturnAsync(int loanId);
    }
}
