using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250)
                .When(x => x.Title != null);

            RuleFor(x => x.ISBN)
                .MaximumLength(50)
                .When(x => x.ISBN != null && x.ISBN.Trim().Length > 0);

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(0, DateTime.UtcNow.Year)
                .When(x => x.PublicationYear.HasValue);

            RuleFor(x => x.AuthorId).GreaterThan(0).When(x => x.AuthorId.HasValue);
            RuleFor(x => x.CategoryId).GreaterThan(0).When(x => x.CategoryId.HasValue);
            RuleFor(x => x.CopiesAvailable).GreaterThanOrEqualTo(0).When(x => x.CopiesAvailable.HasValue);
        }
    }
}
