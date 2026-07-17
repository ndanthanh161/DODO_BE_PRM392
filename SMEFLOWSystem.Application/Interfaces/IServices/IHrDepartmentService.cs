using SMEFLOWSystem.Application.DTOs.HRDtos;

namespace SMEFLOWSystem.Application.Interfaces.IServices;

public interface IHrDepartmentService
{
    Task<List<DepartmentDto>> GetAccessibleAsync();
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto request);
    Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto request);
    Task DeleteAsync(Guid id);
}
