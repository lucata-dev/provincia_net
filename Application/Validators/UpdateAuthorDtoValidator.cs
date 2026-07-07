using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.FirstName != null);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.LastName != null);

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x.Biography)
                .MaximumLength(2000)
                .When(x => x.Biography != null && x.Biography.Trim().Length > 0);
        }
    }
}
