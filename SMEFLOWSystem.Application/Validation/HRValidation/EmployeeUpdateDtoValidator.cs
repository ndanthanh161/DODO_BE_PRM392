using FluentValidation;
using ShareKernel.Common.Enum;
using SMEFLOWSystem.Application.DTOs.HRDtos;

namespace SMEFLOWSystem.Application.Validation.HRValidation;

public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
{
    private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        StatusEnum.EmployeeWorking,
        StatusEnum.EmployeeResigned,
        StatusEnum.EmployeeOnLeave,
        StatusEnum.EmployeeProbation,
    };

    public EmployeeUpdateDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required")
            .MaximumLength(255);

        RuleFor(x => x)
            .Must(x => (x.DepartmentId.HasValue && x.PositionId.HasValue)
                       || (!x.DepartmentId.HasValue && !x.PositionId.HasValue))
            .WithMessage("DepartmentId và PositionId phải đi cùng nhau");

        RuleFor(x => x.BaseSalary).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Status)
            .NotEmpty()
            .Must(s => AllowedStatuses.Contains(s))
            .WithMessage("Invalid Status");

        When(x => string.Equals(x.Status, StatusEnum.EmployeeResigned, StringComparison.OrdinalIgnoreCase), () =>
        {
            RuleFor(x => x.ResignationDate)
                .NotNull().WithMessage("ResignationDate is required when Status=Resigned");
        });
    }
}
