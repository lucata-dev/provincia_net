using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200)
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .When(x => x.Description != null && x.Description.Trim().Length > 0);
        }
    }
}
