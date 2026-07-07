using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<int> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                PublicationYear = dto.PublicationYear,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                CopiesAvailable = dto.CopiesAvailable
            };

            await bookRepository.AddAsync(book);
            await bookRepository.SaveChangesAsync();
            return book.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book is null) return;
            bookRepository.Remove(book);
            await bookRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await bookRepository.GetAllAsync();
            return books.Select(MapToDto).ToList();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            return book is null ? null : MapToDto(book);
        }

        public async Task UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book is null) return;

            if (dto.Title is not null) book.Title = dto.Title;
            if (dto.ISBN is not null) book.ISBN = dto.ISBN;
            if (dto.PublicationYear.HasValue) book.PublicationYear = dto.PublicationYear.Value;
            if (dto.AuthorId.HasValue) book.AuthorId = dto.AuthorId.Value;
            if (dto.CategoryId.HasValue) book.CategoryId = dto.CategoryId.Value;
            if (dto.CopiesAvailable.HasValue) book.CopiesAvailable = dto.CopiesAvailable.Value;

            bookRepository.Update(book);
            await bookRepository.SaveChangesAsync();
        }

        public async Task LoanAsync(int bookId, string borrowerName, DateTime dueDate)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            if (book is null) throw new InvalidOperationException("Libro no encontrado");
            if (book.CopiesAvailable <= 0) throw new InvalidOperationException("No hay copias disponibles");

            var loan = new Loan
            {
                BookId = book.Id,
                BorrowerName = borrowerName,
                LoanDate = DateTime.UtcNow,
                DueDate = dueDate
            };

            book.Loans.Add(loan);
            book.CopiesAvailable -= 1;

            bookRepository.Update(book);
            await bookRepository.SaveChangesAsync();
        }

        public async Task ReturnAsync(int loanId)
        {
            var prestamo = await bookRepository.GetLoanByIdAsync(loanId);
            if (prestamo is null) throw new InvalidOperationException("Préstamo no encontrado");
            if (prestamo.IsReturned) return;

            prestamo.ReturnedDate = DateTime.UtcNow;
            // Aumentar las copias disponibles
            if (prestamo.Book is not null)
            {
                prestamo.Book.CopiesAvailable += 1;
                bookRepository.Update(prestamo.Book);
            }

            await bookRepository.SaveChangesAsync();
        }

        private static BookDto MapToDto(Book b)
        {
            return new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublicationYear = b.PublicationYear,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
                CopiesAvailable = b.CopiesAvailable,
                AuthorName = b.Author is null ? null : $"{b.Author.FirstName} {b.Author.LastName}",
                CategoryName = b.Category?.Name
            };
        }
    }
}
