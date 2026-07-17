using SharedKernel.DTOs;
using SMEFLOWSystem.Application.DTOs.SystemDtos;

namespace SMEFLOWSystem.Application.Interfaces.IServices.System;

public interface ISystemTenantService
{
    Task<PagedResultDto<SystemTenantDto>> GetAllAsync(PagingRequestDto request);
    Task<SystemTenantDto?> GetByIdAsync(Guid tenantId);
}
