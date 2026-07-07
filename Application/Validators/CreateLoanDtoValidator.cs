using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class CreateLoanDtoValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanDtoValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);

            RuleFor(x => x.BorrowerName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.DueDate)
                .Must(d => d > DateTime.UtcNow)
                .WithMessage("DueDate must be in the future");
        }
    }
}
