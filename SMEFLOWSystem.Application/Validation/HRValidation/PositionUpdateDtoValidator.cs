using FluentValidation;
using SMEFLOWSystem.Application.DTOs.HRDtos;

namespace SMEFLOWSystem.Application.Validation.HRValidation;

public class PositionUpdateDtoValidator : AbstractValidator<PositionUpdateDto>
{
    public PositionUpdateDtoValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("DepartmentId is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must be <= 100 characters");
    }
}
