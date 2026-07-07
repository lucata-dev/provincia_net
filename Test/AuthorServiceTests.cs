using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Services;
using Application.DTOs;

namespace Test
{
    public class AuthorServiceTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_Get_Update_Delete_Author()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new AuthorRepository(ctx);
            var svc = new AuthorService(repo);

            var createDto = new CreateAuthorDto { FirstName = "Ana", LastName = "Gomez", BirthDate = DateTime.UtcNow.AddYears(-30), Biography = "Bio" };
            var id = await svc.CreateAsync(createDto);

            var author = await svc.GetByIdAsync(id);
            Assert.NotNull(author);
            Assert.Equal("Ana", author!.FirstName);

            var all = (await svc.GetAllAsync()).ToList();
            Assert.Single(all);

            var updateDto = new Application.DTOs.UpdateAuthorDto { FirstName = "Ana Maria" };
            await svc.UpdateAsync(id, updateDto);

            var updated = await svc.GetByIdAsync(id);
            Assert.Equal("Ana Maria", updated!.FirstName);

            await svc.DeleteAsync(id);
            var afterDelete = await svc.GetByIdAsync(id);
            Assert.Null(afterDelete);
        }
    }
}
