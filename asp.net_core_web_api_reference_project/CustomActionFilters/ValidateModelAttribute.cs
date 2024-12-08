using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp.net_core_web_api_reference_project.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false) // here context se ModelState ko access kar rhe hai
            {
                context.Result = new BadRequestResult(); // this is bad request 400
            }
        }
    }
}
