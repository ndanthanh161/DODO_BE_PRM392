using Microsoft.EntityFrameworkCore;
using SMEFLOWSystem.Application.Interfaces.IRepositories;
using SMEFLOWSystem.Core.Entities;
using SMEFLOWSystem.Infrastructure.Data;

namespace SMEFLOWSystem.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly SMEFLOWSystemContext _context;

    public DepartmentRepository(SMEFLOWSystemContext context)
    {
        _context = context;
    }

    public Task<List<Department>> GetAllAsync()
    {
        return _context.Departments
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public Task<Department?> GetByIdAsync(Guid id)
    {
        return _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task<Department> UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task SoftDeleteAsync(Department department)
    {
        department.IsDeleted = true;
        department.UpdatedAt = DateTime.UtcNow;
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }

    public Task<bool> HasEmployeesAsync(Guid departmentId)
    {
        return _context.Employees.AnyAsync(e => e.DepartmentId == departmentId);
    }

    public Task<int> CountEmployeesAsync(Guid departmentId)
    {
        return _context.Employees.CountAsync(e => e.DepartmentId == departmentId);
    }
}
