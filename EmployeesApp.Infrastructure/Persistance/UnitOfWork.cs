using EmployeesApp.Application;
using EmployeesApp.Application.Employees.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.Infrastructure.Persistance
{
    public class UnitOfWork(
        ApplicationContext context,
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository) : IUnitOfWork
    {
        public IEmployeeRepository Employees => employeeRepository;
        public ICompanyRepository Companies => companyRepository;

        public async Task PersistAllAsync() => await context.SaveChangesAsync();
    }
}
