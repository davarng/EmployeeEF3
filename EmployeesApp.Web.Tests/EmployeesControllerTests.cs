using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using EmployeesApp.Web.Views.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Web.Tests;

public class EmployeesControllerTests
{
    //[Fact]
    //public async Task Index_NoParams_ReturnsViewResultWithCorrectViewModelAsync()
    //{
    //    //Arrange
    //    var employeeService = new Mock<IEmployeeService>();
    //    employeeService.Setup(service => service.GetAllAsync())
    //        .Returns(Task.FromResult<IEnumerable<Employee>>(new[]
    //        {
    //            new Employee{Email = "gmail@gmail.com", Name = "Pär"},
    //            new Employee{Email = "email@email.com", Name = "Name"},
    //            new Employee{Email = "hotmail@hotmail.com", Name = "Namn"}
    //        }));

    //    var employeeController = new EmployeesController(employeeService.Object);

    //    //Act
    //    var result = await employeeController.IndexAsync();

    //    //Assert
    //    var viewResult = Assert.IsType<ViewResult>(result);
    //    Assert.IsType<IndexVM>(viewResult.Model);
    //}

    [Fact]
    public async Task Index_NoParams_ReturnsViewResultWithCorrectViewModel()
    {
        // Arrange
        var employeeService = new Mock<IEmployeeService>();
        employeeService.Setup(service => service.GetAllAsync())
            .Returns(Task.FromResult(new Employee[]
            {
                new Employee { Email = "gmail@gmail.com", Name = "Pär" },
                new Employee { Email = "email@email.com", Name = "Name" },
                new Employee { Email = "hotmail@hotmail.com", Name = "Namn" }
            }));

        var employeeController = new EmployeesController(employeeService.Object);

        // Act
        var result = await employeeController.IndexAsync();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<IndexVM>(viewResult.Model);
    }


    [Theory]
    [InlineData("other@other.com", "other", 4, true)]
    public async Task Create_WithValidViewModel_ReturnsViewResultAsync(string email, string name, int botcheck, bool expected)
    {
        // Arrange
        var viewModel = new CreateVM { Email = email, Name = name, Salary = 0m, BotCheck = botcheck };
        var mockService = new Mock<IEmployeeService>();
        var controller = new EmployeesController(mockService.Object);

        // Act
        var result = await controller.CreateAsync(viewModel);

        // Assert
        if (expected)
        {
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(EmployeesController.IndexAsync).Replace("Async", string.Empty), redirect.ActionName);
            mockService.Verify(s => s.AddAsync(It.Is<Employee>(e =>
                e.Email == email &&
                e.Name == name)), Times.Once);
        }
        else
        {
            var view = Assert.IsType<ViewResult>(result);
            Assert.Null(view.ViewName);
            mockService.Verify(s => s.AddAsync(It.IsAny<Employee>()), Times.Never);
        }
    }

    [Theory]
    [InlineData("name", "email", 4, false)]
    [InlineData("name", "email@mail.com", 3, false)]
    [InlineData("name", "email@mail.com", 4, true)]
    public void Create_ViewModelValidation(string name, string email, int check, bool expect)
    {
        var model = new CreateVM
        {
            Name = name,
            Email = email,
            Salary = 0m,
            BotCheck = check
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

        Assert.Equal(expect, isValid);
    }

    [Fact]
    public async Task Details_WithId_ReturnsViewResult()
    {
        // Arrange
        var employeeService = new Mock<IEmployeeService>();
        var employeeController = new EmployeesController(employeeService.Object);
        var employee = new Employee
        {
            Name = "Alice",
            Email = "alice@example.com"
        };

        employeeService
            .Setup(service => service.GetByIdAsync(1))
            .Returns(Task.FromResult(employee));

        // Act
        var result = await employeeController.DetailsAsync(1);

        // Assert
        Assert.IsType<ViewResult>(result);
        employeeService.Verify(s => s.GetByIdAsync(1), Times.Once);
    }
}
