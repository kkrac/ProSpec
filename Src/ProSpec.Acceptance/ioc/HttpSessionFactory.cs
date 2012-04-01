using System;
using System.Web;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ProSpec.Acceptance.IoC
{
    public class HttpSessionFactory
    {
        public static void RegisterHttpSession(Func<HttpSessionStateBase> function)
        {
            IWindsorContainer container = (IWindsorContainer)IoCProvider.Container;

            container.AddFacility<FactorySupportFacility>();

            container.Register(Component.For<HttpSessionStateBase>()
                        .LifeStyle.PerThread
                        .UsingFactoryMethod<HttpSessionStateBase>(new Function<HttpSessionStateBase>(function)));
        }
    }
}