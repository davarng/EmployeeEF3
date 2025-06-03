using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeesApp.Web.Models
{
    public class MyLogServiceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("---> (ServiceFilter) Action is about to be executed");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("---> (ServiceFilter) Action has now been executed");
        }
    }
}
