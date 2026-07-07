using Infrastructure;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("provinciaNETDb"));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Application.Validators.CreateBookDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<AppDbContext>();

    if (!ctx.Authors.Any() && !ctx.Categories.Any() && !ctx.Books.Any())
    {
        var author1 = new Author
        {
            FirstName = "Gabriel",
            LastName = "García Márquez",
            BirthDate = new DateTime(1927, 3, 6),
            Biography = "Escritor colombiano, premio Nobel de Literatura."
        };

        var author2 = new Author
        {
            FirstName = "Julio",
            LastName = "Cortázar",
            BirthDate = new DateTime(1914, 8, 26),
            Biography = "Escritor argentino, reconocido por su narrativa experimental."
        };

        var cat1 = new Category { Name = "Ficción", Description = "Novelas y relatos de ficción" };
        var cat2 = new Category { Name = "Cuentos", Description = "Antologías y colecciones de cuentos" };

        ctx.Authors.AddRange(author1, author2);
        ctx.Categories.AddRange(cat1, cat2);
        ctx.SaveChanges();

        var book1 = new Book
        {
            Title = "Cien años de soledad",
            ISBN = "978-3-16-148410-0",
            Synopsis = "Saga familiar en Macondo.",
            PublicationYear = 1967,
            AuthorId = author1.Id,
            CategoryId = cat1.Id,
            CopiesAvailable = 3
        };

        var book2 = new Book
        {
            Title = "Rayuela",
            ISBN = "978-84-376-0494-7",
            Synopsis = "Novela experimental sobre el amor y el juego.",
            PublicationYear = 1963,
            AuthorId = author2.Id,
            CategoryId = cat1.Id,
            CopiesAvailable = 2
        };

        ctx.Books.AddRange(book1, book2);
        ctx.SaveChanges();

        var loan1 = new Loan
        {
            BookId = book1.Id,
            BorrowerName = "María Pérez",
            LoanDate = DateTime.UtcNow.AddDays(-3),
            DueDate = DateTime.UtcNow.AddDays(11)
        };

        ctx.Loans.Add(loan1);
        ctx.SaveChanges();
    }
}

app.Run();
