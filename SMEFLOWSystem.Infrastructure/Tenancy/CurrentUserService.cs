using Microsoft.AspNetCore.Http;
using SMEFLOWSystem.SharedKernel.Interfaces;
using System;
using System.Security.Claims;

namespace SMEFLOWSystem.Infrastructure.Tenancy;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return null;

            var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(userIdClaim) && Guid.TryParse(userIdClaim, out var parsed))
            {
                return parsed;
            }

            return null;
        }
    }

    public bool IsInRole(string role)
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.User?.IsInRole(role) == true;
    }
}
