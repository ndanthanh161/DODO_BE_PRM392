using SharedKernel.DTOs;
using SMEFLOWSystem.Application.DTOs.RoleDtos;
using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<Role> AddRoleAsync(RoleUpdatedDto role);
        Task<Role> UpdateRoleAsync(int id, RoleUpdatedDto updatedDto);
        Task<Role> GetRoleByIdAsync(int id);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<PagedResultDto<RoleDto>> GetAllRolesPagingAsync(PagingRequestDto request);
    }
}
