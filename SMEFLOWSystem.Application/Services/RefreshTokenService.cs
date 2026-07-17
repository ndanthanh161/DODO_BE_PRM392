using Microsoft.Extensions.Configuration;
using SMEFLOWSystem.Application.DTOs.RefreshTokenDtos;
using SMEFLOWSystem.Application.Helpers;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Application.Interfaces.IServices;
using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private const int DefaultRefreshTokenDays = 30;

    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IConfiguration config)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<RefreshTokenResponseDto> IssueAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdIgnoreTenantAsync(userId);
        if (user == null)
            throw new ArgumentException("Không tìm thấy user");

        var (rawToken, tokenEntity) = CreateRefreshToken(user);
        await _refreshTokenRepository.AddAsync(tokenEntity);

        var accessToken = AuthHelper.GenerateJwtToken(user, _config);

        return new RefreshTokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = rawToken
        };
    }

    public async Task<(bool success, RefreshTokenResponseDto? response, string message)> RefreshAsync(RefreshRequestDto request)
    {
        var raw = (request?.RefreshToken ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(raw))
            return (false, null, "RefreshToken là bắt buộc");

        var hash = HashToken(raw);
        var existing = await _refreshTokenRepository.GetByTokenHashIgnoreTenantAsync(hash);
        if (existing == null)
            return (false, null, "RefreshToken không hợp lệ");

        if (existing.RevokedAt != null)
            return (false, null, "RefreshToken đã bị thu hồi");

        if (existing.ExpiresAt <= DateTime.UtcNow)
            return (false, null, "RefreshToken đã hết hạn");

        var user = await _userRepository.GetByIdIgnoreTenantAsync(existing.UserId);
        if (user == null)
            return (false, null, "Không tìm thấy user");

        // Rotate token
        var (newRaw, newToken) = CreateRefreshToken(user);
        newToken.TenantId = existing.TenantId;

        await _refreshTokenRepository.AddAsync(newToken);

        existing.RevokedAt = DateTime.UtcNow;
        existing.ReplacedByTokenId = newToken.Id;
        existing.RevokeReason = "Rotated";
        await _refreshTokenRepository.UpdateAsync(existing);

        var accessToken = AuthHelper.GenerateJwtToken(user, _config);

        return (true, new RefreshTokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRaw
        }, string.Empty);
    }

    public async Task RevokeAllAsync(Guid userId, string reason)
    {
        var user = await _userRepository.GetByIdIgnoreTenantAsync(userId);
        if (user == null)
            throw new ArgumentException("Không tìm thấy user");

        await _refreshTokenRepository.RevokeAllAsync(user.Id, user.TenantId, string.IsNullOrWhiteSpace(reason) ? "Revoked" : reason);
    }

    public async Task<List<RefreshTokenDto>> GetAllByUserIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("Không tìm thấy user");

        var tokens = await _refreshTokenRepository.GetByUserIdAsync(user.Id, user.TenantId);
        return tokens.Select(t => new RefreshTokenDto
        {
            Id = t.Id,
            CreatedAt = t.CreatedAt,
            ExpiresAt = t.ExpiresAt,
            RevokedAt = t.RevokedAt
        }).ToList();
    }

    private (string rawToken, RefreshToken entity) CreateRefreshToken(User user)
    {
        var raw = GenerateSecureToken();
        var hash = HashToken(raw);
        var now = DateTime.UtcNow;

        var days = GetRefreshTokenDays();

        var entity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            TenantId = user.TenantId,
            UserId = user.Id,
            TokenHash = hash,
            CreatedAt = now,
            ExpiresAt = now.AddDays(days),
            RevokedAt = null,
            ReplacedByTokenId = null,
            RevokeReason = null
        };

        return (raw, entity);
    }

    private int GetRefreshTokenDays()
    {
        // Optional config: Jwt:RefreshTokenDays
        var raw = _config["Jwt:RefreshTokenDays"];
        if (int.TryParse(raw, out var days) && days > 0)
            return days;
        return DefaultRefreshTokenDays;
    }

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    private static string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));

        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}
