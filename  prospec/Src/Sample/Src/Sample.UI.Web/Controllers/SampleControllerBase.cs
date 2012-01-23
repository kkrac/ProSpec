using System.Web.Mvc;

namespace Sample.UI.Web.Controllers
{
    public abstract class SampleControllerBase : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                //Log.Error(filterContext.Exception);

                ModelState.AddModelError(string.Empty/*filterContext.ActionDescriptor.ActionName*/, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;

                filterContext.Result = View();
            }
        }
    }
}