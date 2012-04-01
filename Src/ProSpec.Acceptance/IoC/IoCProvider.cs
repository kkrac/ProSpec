using System;
using System.Collections.Generic;
using Castle.Windsor;

namespace ProSpec.Acceptance.IoC
{
    /// <summary>
    /// Inversion of control helpers.
    /// </summary>
    public class IoCProvider
    {
        private static IWindsorContainer container;
        private static readonly object LocalContainerKey = new object();
        public delegate void ContainerInitializedEventHandler();
        public static event ContainerInitializedEventHandler Initialized;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            Initialize(new ProSpecContainer());

            if (Initialized != null)
            {
                Initialized();
            }
        }

        /// <summary>
        /// Initializes this instance with the specified windsor container.
        /// </summary>
        /// <param name="windsorContainer">The windsor container.</param>
        public static void Initialize(IWindsorContainer windsorContainer)
        {
            GlobalContainer = windsorContainer;
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>Returns an instance of the specified service.</returns>
        public static object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>Returns an instance of the specified service.</returns>
        public static object Resolve(string serviceName)
        {
            return Container.Resolve(serviceName);
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>Returns an instance of the specified service.</returns>
        public static object Resolve(Type serviceType, string serviceName)
        {
            return Container.Resolve(serviceName, serviceType);
        }

        /// <summary>
        /// Tries to resolve the component, but return null
        /// instead of throwing if it is not there.
        /// Useful for optional dependencies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T TryResolve<T>()
        {
            return TryResolve<T>(default(T));
        }

        /// <summary>
        /// Tries to resolve the compoennt, but return the default 
        /// value if could not find it, instead of throwing.
        /// Useful for optional dependencies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryResolve<T>(T defaultValue)
        {
            if (Container.Kernel.HasComponent(typeof(T)) == false)
                return defaultValue;
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified service based on its type.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <returns>Returns an instance of the service.</returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified service based on its type.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>Returns an instance of the service.</returns>
        public static T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public static IWindsorContainer Container
        {
            get
            {
                IWindsorContainer result = LocalContainer ?? GlobalContainer;
                if (result == null)
                    throw new InvalidOperationException("The container has not been initialized!");
                return result;
            }
        }

        /// <summary>
        /// Gets the local container.
        /// </summary>
        /// <value>The local container.</value>
        private static IWindsorContainer LocalContainer
        {
            get
            {
                if (LocalContainerStack.Count == 0)
                    return null;
                return LocalContainerStack.Peek();
            }
        }

        /// <summary>
        /// Gets the local container stack.
        /// </summary>
        /// <value>The local container stack.</value>
        private static Stack<IWindsorContainer> LocalContainerStack
        {
            get
            {
                Stack<IWindsorContainer> stack = Local.Data[LocalContainerKey] as Stack<IWindsorContainer>;
                if (stack == null)
                {
                    Local.Data[LocalContainerKey] = stack = new Stack<IWindsorContainer>();
                }
                return stack;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInitialized
        {
            get { return GlobalContainer != null; }
        }

        /// <summary>
        /// Gets or sets the global container.
        /// </summary>
        /// <value>The global container.</value>
        internal static IWindsorContainer GlobalContainer
        {
            get { return container; }
            set { container = value; }
        }

        //public static IDisposable UseLocalContainer(IWindsorContainer localContainer)
        //{
        //    LocalContainerStack.Push(localContainer);
        //    return new DisposableAction(delegate
        //    {
        //        Reset(localContainer);
        //    });
        //}

        /// <summary>
        /// Resets the specified container.
        /// </summary>
        /// <param name="containerToReset">The container to reset.</param>
        public static void Reset(IWindsorContainer containerToReset)
        {
            if (containerToReset == null)
                return;

            if (ReferenceEquals(LocalContainer, containerToReset))
            {
                LocalContainerStack.Pop();
                if (LocalContainerStack.Count == 0)
                    Local.Data[LocalContainerKey] = null;
                return;
            }

            if (ReferenceEquals(GlobalContainer, containerToReset))
            {
                GlobalContainer = null;
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            IWindsorContainer windsorContainer = LocalContainer ?? GlobalContainer;
            Reset(windsorContainer);
        }

        public static void Release(object objectToRelease)
        {
            Container.Release(objectToRelease);
        }
    }
}