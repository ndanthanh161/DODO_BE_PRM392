using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Core.Entities;
using SMEFLOWSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly SMEFLOWSystemContext _context;

    public RefreshTokenRepository(SMEFLOWSystemContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public Task<RefreshToken?> GetByTokenHashIgnoreTenantAsync(string tokenHash)
    {
        return _context.RefreshTokens
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public Task<List<RefreshToken>> GetByUserIdAsync(Guid userId, Guid tenantId)
    {
        return _context.RefreshTokens
            .AsNoTracking()
            .Where(t => t.UserId == userId && t.TenantId == tenantId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task RevokeAllAsync(Guid userId, Guid tenantId, string reason)
    {
        var now = DateTime.UtcNow;

        var tokens = await _context.RefreshTokens
            .Where(t => t.UserId == userId && t.TenantId == tenantId && t.RevokedAt == null)
            .ToListAsync();

        if (tokens.Count == 0) return;

        foreach (var token in tokens)
        {
            token.RevokedAt = now;
            token.RevokeReason = reason;
        }

        await _context.SaveChangesAsync();
    }
}
