using SMEFLOWSystem.Core.Entities;

namespace SMEFLOWSystem.Application.Interfaces.IRepositories;

public interface IPositionRepository
{
    Task<List<Position>> GetByDepartmentIdAsync(Guid departmentId);
    Task<Position?> GetByIdAsync(Guid id);
    Task AddAsync(Position position);
    Task<Position> UpdateAsync(Position position);
    Task SoftDeleteAsync(Position position);
    Task<bool> HasEmployeesAsync(Guid positionId);
}
