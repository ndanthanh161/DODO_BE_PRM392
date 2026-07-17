using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Core.Entities;
using SMEFLOWSystem.Infrastructure.Data;

namespace SMEFLOWSystem.Infrastructure.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly SMEFLOWSystemContext _context;

    public PositionRepository(SMEFLOWSystemContext context)
    {
        _context = context;
    }

    public Task<List<Position>> GetByDepartmentIdAsync(Guid departmentId)
    {
        return _context.Positions
            .Where(p => p.DepartmentId == departmentId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public Task<Position?> GetByIdAsync(Guid id)
    {
        return _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Position position)
    {
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();
    }

    public async Task<Position> UpdateAsync(Position position)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync();
        return position;
    }

    public async Task SoftDeleteAsync(Position position)
    {
        position.IsDeleted = true;
        position.UpdatedAt = DateTime.UtcNow;
        _context.Positions.Update(position);
        await _context.SaveChangesAsync();
    }

    public Task<bool> HasEmployeesAsync(Guid positionId)
    {
        return _context.Employees.AnyAsync(e => e.PositionId == positionId);
    }

    public Task<int> CountEmployeesAsync(Guid positionId)
    {
        return _context.Employees.CountAsync(e => e.PositionId == positionId);
    }
}
