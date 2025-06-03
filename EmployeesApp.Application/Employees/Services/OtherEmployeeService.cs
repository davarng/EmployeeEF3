using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Application.Employees.Services;

public class OtherEmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task AddAsync(Employee employee)
    {
        await employeeRepository.AddAsync(employee);
    }

    public async Task<Employee[]> GetAllAsync()
    {
        return await employeeRepository.GetAllAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id) => await employeeRepository.GetByIdAsync(id);

    public bool CheckIsVIP(Employee employee) =>
        employee.Email.StartsWith("ADMIN", StringComparison.CurrentCultureIgnoreCase);
}