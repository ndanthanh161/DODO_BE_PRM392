namespace SMEFLOWSystem.Application.DTOs.HRDtos;

public class PositionCreateDto
{
    public Guid DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}
