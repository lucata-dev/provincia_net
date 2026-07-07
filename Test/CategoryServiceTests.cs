using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Services;
using Application.DTOs;

namespace Test
{
    public class CategoryServiceTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_Get_Update_Delete_Category()
        {
            var dbName = Guid.NewGuid().ToString();
            using var ctx = CreateContext(dbName);
            var repo = new CategoryRepository(ctx);
            var svc = new CategoryService(repo);

            var createDto = new CreateCategoryDto { Name = "Novela", Description = "Narrativa" };
            var id = await svc.CreateAsync(createDto);

            var cat = await svc.GetByIdAsync(id);
            Assert.NotNull(cat);
            Assert.Equal("Novela", cat!.Name);

            var all = (await svc.GetAllAsync()).ToList();
            Assert.Single(all);

            var updateDto = new UpdateCategoryDto { Description = "Narrativa extensa" };
            await svc.UpdateAsync(id, updateDto);

            var updated = await svc.GetByIdAsync(id);
            Assert.Equal("Narrativa extensa", updated!.Description);

            await svc.DeleteAsync(id);
            var afterDelete = await svc.GetByIdAsync(id);
            Assert.Null(afterDelete);
        }
    }
}
