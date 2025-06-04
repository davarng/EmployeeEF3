using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Application.Employees.Services;

public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
{
    public async Task AddAsync(Employee employee)
    {
        employee.Name = ToInitalCapital(employee.Name);
        employee.Email = employee.Email.ToLower();
        await unitOfWork.Employees.AddAsync(employee);
    }

    string ToInitalCapital(string s) => $"{s[..1].ToUpper()}{s[1..]}";


    public async Task<Employee[]> GetAllAsync()
    {
        var employees = await unitOfWork.Employees.GetAllAsync();
        return employees.OrderBy(e => e.Name).ToArray();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        Employee? employee = await unitOfWork.Employees.GetByIdAsync(id);

        return employee is null ?
            throw new ArgumentException($"Invalid parameter value: {id}", nameof(id)) :
            employee;
    }

    public bool CheckIsVIP(Employee employee) =>
        employee.Email.StartsWith("ANDERS", StringComparison.CurrentCultureIgnoreCase);
}