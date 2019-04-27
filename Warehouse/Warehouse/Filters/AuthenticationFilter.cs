namespace Warehouse.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    //public class AuthenticationFilter : IActionFilter
    //{
    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        var param = context.ActionArguments.SingleOrDefault(p => p.Value is IEntity);
    //        if (param.Value == null)
    //        {
    //            context.Result = new BadRequestObjectResult("Not Authorized!");
    //            return;
    //        }

    //        if (!context.ModelState.IsValid)
    //        {
    //            context.Result = new BadRequestObjectResult(context.ModelState);
    //        }
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        //throw new System.NotImplementedException();
    //    }
    //}
}