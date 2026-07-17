namespace SMEFLOWSystem.Application.DTOs.HRDtos;

public class PositionDto
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}
