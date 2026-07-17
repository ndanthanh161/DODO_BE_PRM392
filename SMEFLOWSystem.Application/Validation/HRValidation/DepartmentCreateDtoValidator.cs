using FluentValidation;
using SMEFLOWSystem.Application.DTOs.HRDtos;

namespace SMEFLOWSystem.Application.Validation.HRValidation;

public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must be <= 100 characters");
    }
}
