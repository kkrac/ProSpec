using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using TwoK.Core.IoC;

namespace Sample.UI.Web.Controllers
{
    public class ControllerFactory : DefaultControllerFactory
    {
        public override void ReleaseController(IController controller)
        {
            IoCProvider.Release(controller);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, System.Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return IoCProvider.Resolve<IController>(controllerType.Name);
        }
    }
}