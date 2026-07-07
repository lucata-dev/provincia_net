using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Services;
using Application.DTOs;

namespace Test
{
    public class LoanServiceTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_Get_Return_Delete_Loan()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);

            // Crear autor, categoría y libro
            var authorRepo = new AuthorRepository(ctx);
            var authorSvc = new Application.Services.AuthorService(authorRepo);
            var authorId = await authorSvc.CreateAsync(new CreateAuthorDto { FirstName = "A", LastName = "B", BirthDate = DateTime.UtcNow.AddYears(-50) });

            var categoryRepo = new CategoryRepository(ctx);
            var categorySvc = new Application.Services.CategoryService(categoryRepo);
            var catId = await categorySvc.CreateAsync(new CreateCategoryDto { Name = "N", Description = "D" });

            var bookRepo = new BookRepository(ctx);
            var bookSvc = new BookService(bookRepo);
            var bookId = await bookSvc.CreateAsync(new CreateBookDto { Title = "L", PublicationYear = 2010, AuthorId = authorId, CategoryId = catId, CopiesAvailable = 2, ISBN = Guid.NewGuid().ToString()});

            var loanRepo = new LoanRepository(ctx);
            var loanSvc = new LoanService(loanRepo);

            var createLoan = new CreateLoanDto { BookId = bookId, BorrowerName = "Z", DueDate = DateTime.UtcNow.AddDays(5) };
            var loanId = await loanSvc.CreateAsync(createLoan);

            var loan = await loanSvc.GetByIdAsync(loanId);
            Assert.NotNull(loan);
            Assert.Equal("Z", loan!.BorrowerName);

            // Return
            await loanSvc.ReturnAsync(loanId);
            var after = await loanSvc.GetByIdAsync(loanId);
            Assert.NotNull(after!.ReturnedDate);

            // Delete
            await loanSvc.DeleteAsync(loanId);
            var afterDelete = await loanSvc.GetByIdAsync(loanId);
            Assert.Null(afterDelete);
        }
    }
}
