using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext context;

        public BookRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Loans)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Loans)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Remove(Book book)
        {
            context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);
        }

        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await context.Loans
                .Include(p => p.Book)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
