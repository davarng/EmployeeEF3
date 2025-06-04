using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeesApp.Infrastructure.Persistance.Repositories
{
    public class CompanyRepository(ApplicationContext context) : ICompanyRepository
    {
        // TODO: The interface has been updated as well
        public void Add(Company company)
        {
            context.Companies.Add(company);
        }

        // TODO: Added AsNoTracking() & Include()
        public async Task<Company[]> GetAllAsync() =>
            await context.Companies.Include(o => o.Employees).ToArrayAsync();

        public async Task<Company?> GetByIdAsync(int id) => await context.Companies
            .FindAsync(id);

        public async Task DeleteAsync(int id)
        {
            var c = await GetByIdAsync(id);
            context.Companies.Remove(c);

        }


    }
}