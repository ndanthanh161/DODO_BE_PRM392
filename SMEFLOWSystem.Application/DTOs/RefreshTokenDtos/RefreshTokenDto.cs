using System;

namespace SMEFLOWSystem.Application.DTOs.RefreshTokenDtos;

public class RefreshTokenDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
}
