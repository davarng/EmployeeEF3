using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Infrastructure.Persistance.Repositories
{
    public class EmployeeRepository(ApplicationContext context) : IEmployeeRepository
    {
        // TODO: The interface has been updated as well
        public async Task AddAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync(); // Inte glömma!
        }

        // TODO: Added AsNoTracking() & Include()
        public async Task<Employee[]> GetAllAsync() =>
            await context.Employees.AsNoTracking().Include(o => o.Company).ToArrayAsync();

        public async Task<Employee?> GetByIdAsync(int id) => await context.Employees
            .FindAsync(id);
    }
}