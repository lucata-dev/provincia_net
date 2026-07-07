using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x.Biography)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrWhiteSpace(x.Biography));
        }
    }
}
