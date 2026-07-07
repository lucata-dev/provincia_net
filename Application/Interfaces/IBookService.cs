using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDto?> GetByIdAsync(int id);

        Task<IEnumerable<BookDto>> GetAllAsync();

        Task<int> CreateAsync(CreateBookDto dto);

        Task UpdateAsync(int id, UpdateBookDto dto);

        Task DeleteAsync(int id);

        Task LoanAsync(int bookId, string borrowerName, DateTime dueDate);

        Task ReturnAsync(int prestamoId);
    }
}
