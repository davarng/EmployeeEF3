using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Application.Employees.Interfaces
{
    public interface ICompanyRepository
    {
        void Add(Company company);

        Task<Company[]> GetAllAsync();

        Task<Company?> GetByIdAsync(int id);

        Task DeleteAsync (int id);
    }
}