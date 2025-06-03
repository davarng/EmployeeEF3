using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Application.Employees.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task AddAsync(Employee employee)
    {
        employee.Name = ToInitalCapital(employee.Name);
        employee.Email = employee.Email.ToLower();
        await employeeRepository.AddAsync(employee);
    }

    string ToInitalCapital(string s) => $"{s[..1].ToUpper()}{s[1..]}";


    public async Task<Employee[]> GetAllAsync()
    {
        var employees = await employeeRepository.GetAllAsync();
        return employees.OrderBy(e => e.Name).ToArray();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        Employee? employee = await employeeRepository.GetByIdAsync(id);

        return employee is null ?
            throw new ArgumentException($"Invalid parameter value: {id}", nameof(id)) :
            employee;
    }

    public bool CheckIsVIP(Employee employee) =>
        employee.Email.StartsWith("ANDERS", StringComparison.CurrentCultureIgnoreCase);
}