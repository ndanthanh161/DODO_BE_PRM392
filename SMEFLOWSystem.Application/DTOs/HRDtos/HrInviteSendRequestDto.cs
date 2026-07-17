namespace SMEFLOWSystem.Application.DTOs.HRDtos;

public class HrInviteSendRequestDto
{
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public string Message { get; set; } = string.Empty;
}
