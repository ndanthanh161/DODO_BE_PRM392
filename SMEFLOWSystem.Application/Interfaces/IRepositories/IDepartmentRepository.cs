using SMEFLOWSystem.Core.Entities;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(Guid id);
    Task AddAsync(Department department);
    Task<Department> UpdateAsync(Department department);
    Task SoftDeleteAsync(Department department);
    Task<bool> HasEmployeesAsync(Guid departmentId);
    Task<int> CountEmployeesAsync(Guid departmentId);
}
