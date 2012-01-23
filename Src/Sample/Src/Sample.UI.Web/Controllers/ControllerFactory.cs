using System.Web.Mvc;
using System.Web.Routing;
using TwoK.Core.IoC;
using System.Web.SessionState;

namespace Sample.UI.Web.Controllers
{
    public class ControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return IoCProvider.Resolve<IController>(controllerName + "Controller");
        }

        public void ReleaseController(IController controller)
        {
            IoCProvider.Release(controller);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
    }
}