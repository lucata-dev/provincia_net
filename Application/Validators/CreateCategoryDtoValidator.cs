using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .When(x => x.Description != null && x.Description.Trim().Length > 0);
        }
    }
}
