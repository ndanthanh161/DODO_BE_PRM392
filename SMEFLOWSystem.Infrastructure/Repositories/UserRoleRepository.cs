using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Core.Entities;
using SMEFLOWSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SMEFLOWSystemContext _context;

        public UserRoleRepository(SMEFLOWSystemContext context)
        {
            _context = context;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveUserRoleAsync(Guid userId, int roleId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)            
                return false;
            
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ReplaceUserRolesAsync(Guid userId, Guid tenantId, IEnumerable<int> roleIds)
        {
            var ids = roleIds?.Distinct().ToList() ?? new List<int>();

            var existing = await _context.UserRoles
                .Where(ur => ur.UserId == userId && ur.TenantId == tenantId)
                .ToListAsync();

            if (existing.Count > 0)
                _context.UserRoles.RemoveRange(existing);

            if (ids.Count > 0)
            {
                var newRoles = ids.Select(roleId => new UserRole
                {
                    UserId = userId,
                    TenantId = tenantId,
                    RoleId = roleId
                });

                await _context.UserRoles.AddRangeAsync(newRoles);
            }

            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyUserHasRoleIgnoreTenantAsync(int roleId)
        {
            return _context.UserRoles
                .IgnoreQueryFilters()
                .AnyAsync(ur => ur.RoleId == roleId);
        }
    }
}
