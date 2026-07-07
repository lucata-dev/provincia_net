using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.ISBN)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.ISBN));

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(0, DateTime.UtcNow.Year);

            RuleFor(x => x.AuthorId).GreaterThan(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0);
        }
    }
}
