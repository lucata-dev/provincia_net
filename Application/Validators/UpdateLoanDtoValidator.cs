using System;
using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class UpdateLoanDtoValidator : AbstractValidator<UpdateLoanDto>
    {
        public UpdateLoanDtoValidator()
        {
            RuleFor(x => x.DueDate)
                .Must(d => d.HasValue ? d.Value > DateTime.UtcNow : true)
                .WithMessage("DueDate must be in the future");

            RuleFor(x => x.ReturnedDate)
                .Must(d => d.HasValue ? d.Value <= DateTime.UtcNow : true)
                .WithMessage("ReturnedDate cannot be in the future");
        }
    }
}
