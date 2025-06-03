using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Web.Models
{
    public class SuspiciousPersonAttribute(string name) : ValidationAttribute
    {
        public override bool IsValid(object? value) =>
            value?.ToString()?.ToUpper() != name.ToUpper();
    }
}
