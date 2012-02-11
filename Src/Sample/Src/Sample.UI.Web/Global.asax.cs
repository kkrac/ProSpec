using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TwoK.Core.IoC;
using Sample.UI.Web.Controllers;

namespace Sample.UI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class SampleApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ConfirmAccount", // Route name
                "UserAccount/Confirm/{userId}/{token}",
                new { controller = "UserAccount", action = "Confirm" }
            );

            routes.MapRoute(
                "Login", // Route name
                "UserAccount/Login",
                new { controller = "UserAccount", action = "Login" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}",
                new { controller = "UserAccount", action = "Login" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterRoutes(RouteTable.Routes);
            
            InitializeContainer();
            
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
        }

        private void InitializeContainer()
        {
            IoCProvider.Initialize();
            
            HttpSessionFactory.RegisterHttpSession(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
        }
    }
}