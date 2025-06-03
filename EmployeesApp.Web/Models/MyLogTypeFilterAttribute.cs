using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeesApp.Web.Models
{
    public class MyLogTypeFilterAttribute(ILogger<MyLogTypeFilterAttribute> logger) : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("---> Action is about to be executed");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("---> Action has now been executed");
        }
    }
}
