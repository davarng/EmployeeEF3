using EmployeesApp.Application.Employees.Interfaces;

namespace EmployeesApp.Application
{
    public interface IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IEmployeeRepository Employees { get; }

        Task PersistAllAsync();
    }
}