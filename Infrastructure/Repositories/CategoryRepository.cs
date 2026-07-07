using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Category category)
        {
            await this.context.Categories.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await this.context.Categories
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await this.context.Categories
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Remove(Category category)
        {

            this.context.Categories.Remove(category);
        }

        public Task SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            this.context.Categories.Update(category);
        }
    }
}
