using EmployeesApp.Application.Employees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Web.Controllers
{
    public class CompanyController(ICompanyService service) : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
