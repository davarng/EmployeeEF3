using EmployeesApp.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Web.Views.Employees
{
    public class CreateVM
    {
        [Required(ErrorMessage = "You must specify a name")]
        [SuspiciousPerson("Pontus Wittenmark", ErrorMessage = "Suspicious person detected!")]
        public required string Name { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        [Required(ErrorMessage = "You must specify an e-mail address")]
        public required string Email { get; set; }

        [Display(Name = "Salary")]
        [Required(ErrorMessage = "You must specify a salary")]
        public required decimal Salary { get; set; }

        [Display(Name = "2 + 2 = ?")]
        [Required(ErrorMessage = "You must answer the question")]
        [Range(4, 4, ErrorMessage = "Wrong answer")]
        public required int BotCheck { get; set; }
    }
}
