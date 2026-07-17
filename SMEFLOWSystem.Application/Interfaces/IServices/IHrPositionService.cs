using SMEFLOWSystem.Application.DTOs.HRDtos;

namespace SMEFLOWSystem.Application.Interfaces.IServices;

public interface IHrPositionService
{
    Task<List<PositionDto>> GetByDepartmentAsync(Guid departmentId);
    Task<PositionDto> CreateAsync(PositionCreateDto request);
    Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto request);
    Task DeleteAsync(Guid id);
}
