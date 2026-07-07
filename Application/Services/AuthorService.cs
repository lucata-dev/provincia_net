using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository categoryRepository;

        public AuthorService(IAuthorRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Biography = dto.Biography
            };

            await this.categoryRepository.AddAsync(author);
            await this. categoryRepository.SaveChangesAsync();
            return author.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await this.categoryRepository.GetByIdAsync(id);
            if (author is null) return;
            this.categoryRepository.Remove(author);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await this.categoryRepository.GetAllAsync();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
                Biography = a.Biography
            }).ToList();
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var a = await this.categoryRepository.GetByIdAsync(id);
            if (a is null) return null;
            return new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
                Biography = a.Biography
            };
        }

        public async Task UpdateAsync(int id, UpdateAuthorDto dto)
        {
            var author = await this.categoryRepository.GetByIdAsync(id);
            if (author is null) return;

            if (dto.FirstName is not null) author.FirstName = dto.FirstName;
            if (dto.LastName is not null) author.LastName = dto.LastName;
            if (dto.BirthDate.HasValue) author.BirthDate = dto.BirthDate.Value;
            if (dto.Biography is not null) author.Biography = dto.Biography;

            this.categoryRepository.Update(author);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
