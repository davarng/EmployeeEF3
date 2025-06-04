using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using EmployeesApp.Application.Employees.Interfaces;

namespace EmployeesApp.Terminal;
internal class Program
{
    static EmployeeService employeeService;

    static async Task Main(string[] args)
    {
        string connectionString;

        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", false);
        var app = builder.Build();
        connectionString = app.GetConnectionString("DefaultConnection");

        // Configure DbContext options
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString)
            .Options;

        // Create and use the context

        var context = new ApplicationContext(options, NullLogger<ApplicationContext>.Instance);
        ICompanyRepository companyRepository = new CompanyRepository(context);
        IEmployeeRepository employeeRepository = new EmployeeRepository(context);
        employeeService = new(new UnitOfWork(context, companyRepository, employeeRepository));

        await ListAllEmployeesAsync();
        await ListEmployeeAsync(562);
    }

    private static async Task ListAllEmployeesAsync()
    {
        foreach (var item in await employeeService.GetAllAsync())
            Console.WriteLine(item.Name);

        Console.WriteLine("------------------------------");
    }

    private static async Task ListEmployeeAsync(int employeeID)
    {
        Employee? employee;

        try
        {
            employee = await employeeService.GetByIdAsync(employeeID);
            Console.WriteLine($"{employee?.Name}: {employee?.Email}");
            Console.WriteLine("------------------------------");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"EXCEPTION: {e.Message}");
        }
    }
}
