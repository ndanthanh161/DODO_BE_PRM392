namespace SMEFLOWSystem.Application.DTOs.HRDtos;

public class EmployeeUpdateDto
{
    public Guid? UserId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public DateOnly HireDate { get; set; }
    public DateOnly? ResignationDate { get; set; }
    public decimal BaseSalary { get; set; }
    public string Status { get; set; } = string.Empty;
}
