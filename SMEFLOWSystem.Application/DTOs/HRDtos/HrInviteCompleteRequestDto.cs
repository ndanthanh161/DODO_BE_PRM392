namespace SMEFLOWSystem.Application.DTOs.HRDtos;

public class HrInviteCompleteRequestDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Phone { get; set; }
}
