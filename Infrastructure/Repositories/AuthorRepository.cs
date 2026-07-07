using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext context;

        public AuthorRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Author author)
        {
            await this.context.Authors.AddAsync(author);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await this.context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await this.context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Remove(Author author)
        {
            this.context.Authors.Remove(author);
        }

        public void Update(Author author)
        {
            this.context.Authors.Update(author);
        }

        public Task SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }
    }
}
