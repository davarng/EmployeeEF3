using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using Moq;

namespace EmployeeApp.Application.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void AddEmployee_WithRightCredentials_WillAddEmployeeToListMoq()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        var service = new EmployeeService(mockRepo.Object);

        var employee = new Employee
        {
            Name = "lisa",
            Email = "lisa@Ajax.com"
        };

        // Act
        service.AddAsync(employee);

        // Assert
        mockRepo.Verify(o => o.AddAsync(It.Is<Employee>(o =>
            o.Name == "Lisa" &&
            o.Email == "lisa@ajax.com")), Times.Once);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsEmployee()
    {
        // Arrange
        var employee = new Employee { Id = 1, Name = "Ben", Email = "dover@hotmale.com" };
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo
            .Setup(r => r.GetByIdAsync(1))
            .Returns(Task.FromResult(employee)); // .Returns(employee);

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        Assert.Equal(employee, result);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ThrowsArgumentException()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo
            .Setup(r => r.GetByIdAsync(999))
            .Returns(Task.FromResult<Employee?>(null));

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = await Record.ExceptionAsync(() => service.GetByIdAsync(999));

        // Assert
        Assert.IsType<ArgumentException>(result);
        Assert.Equal("Invalid parameter value: 999 (Parameter 'id')", result?.Message);
    }

    [Theory]
    [InlineData("anders@mail.com", true)] // Anders is hardcoded as VIP in EmplooyeeService
    [InlineData("ANDERS@mail.com", true)] // Case insensitive check
    [InlineData("lisa@ajax.com", false)] // Not a VIP email
    [InlineData("LISA@ajax.com", false)] // Case insensitive check   
    public void CheckIsVIP_WithVIPEmail_ReturnsExpectedResult(string email, bool expected)
    {
        var employee = new Employee { Email = email };
        var service = new EmployeeService(Mock.Of<IEmployeeRepository>());

        var result = service.CheckIsVIP(employee);

        Assert.Equal(expected, result);
    }
}
