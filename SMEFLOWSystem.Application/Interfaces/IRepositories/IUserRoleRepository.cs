using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(UserRole userRole);
        Task<bool> RemoveUserRoleAsync(Guid userId, int roleId);
        Task ReplaceUserRolesAsync(Guid userId, Guid tenantId, IEnumerable<int> roleIds);

        Task<bool> AnyUserHasRoleIgnoreTenantAsync(int roleId);

    }
}
