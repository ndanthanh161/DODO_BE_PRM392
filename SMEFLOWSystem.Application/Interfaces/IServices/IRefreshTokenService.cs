using SMEFLOWSystem.Application.DTOs.RefreshTokenDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IServices;

public interface IRefreshTokenService
{
    Task<RefreshTokenResponseDto> IssueAsync(Guid userId);
    Task<(bool success, RefreshTokenResponseDto? response, string message)> RefreshAsync(RefreshRequestDto request);
    Task RevokeAllAsync(Guid userId, string reason);
    Task<List<RefreshTokenDto>> GetAllByUserIdAsync(Guid userId);
}
