using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Services;
using Application.DTOs;

namespace Test
{
    public class BookServiceTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_Get_Update_Delete_Book_And_Loan_Return()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);

            // Necesitamos autor y categoría antes de crear libro
            var authorRepo = new AuthorRepository(ctx);
            var authorSvc = new Application.Services.AuthorService(authorRepo);
            var authorId = await authorSvc.CreateAsync(new CreateAuthorDto { FirstName = "X", LastName = "Y", BirthDate = DateTime.UtcNow.AddYears(-40) });

            var categoryRepo = new CategoryRepository(ctx);
            var categorySvc = new Application.Services.CategoryService(categoryRepo);
            var catId = await categorySvc.CreateAsync(new CreateCategoryDto { Name = "C", Description = "D" });

            var bookRepo = new BookRepository(ctx);
            var bookSvc = new BookService(bookRepo);

            var createDto = new CreateBookDto { Title = "Libro", PublicationYear = 2020, AuthorId = authorId, CategoryId = catId, CopiesAvailable = 1, ISBN = Guid.NewGuid().ToString(), };
            var id = await bookSvc.CreateAsync(createDto);

            var b = await bookSvc.GetByIdAsync(id);
            Assert.NotNull(b);
            Assert.Equal("Libro", b!.Title);

            // Loan
            await bookSvc.LoanAsync(id, "Persona", DateTime.UtcNow.AddDays(7));
            var afterLoan = await bookSvc.GetByIdAsync(id);
            Assert.Equal(0, afterLoan!.CopiesAvailable);

            // Obtener préstamo para retornar (usar LoanRepository directamente)
            var loan = ctx.Loans.FirstOrDefault(l => l.BookId == id);
            Assert.NotNull(loan);

            await bookSvc.ReturnAsync(loan!.Id);
            var afterReturn = await bookSvc.GetByIdAsync(id);
            Assert.Equal(1, afterReturn!.CopiesAvailable);

            // Update
            var updateDto = new UpdateBookDto { Title = "Libro2" };
            await bookSvc.UpdateAsync(id, updateDto);
            var updated = await bookSvc.GetByIdAsync(id);
            Assert.Equal("Libro2", updated!.Title);

            await bookSvc.DeleteAsync(id);
            var afterDelete = await bookSvc.GetByIdAsync(id);
            Assert.Null(afterDelete);
        }
    }
}
