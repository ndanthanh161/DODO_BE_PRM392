using SMEFLOWSystem.Application.DTOs.SystemDtos;

namespace SMEFLOWSystem.Application.Interfaces.IServices.System;

public interface ISystemBootstrapService
{
    Task<(Guid tenantId, Guid userId)> BootstrapAsync(SystemBootstrapRequestDto request);
}
