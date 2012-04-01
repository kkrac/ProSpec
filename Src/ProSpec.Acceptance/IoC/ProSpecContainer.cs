using System;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration;
using Castle.Windsor.Configuration.Interpreters;

namespace ProSpec.Acceptance.IoC
{
    /// <summary>
    /// Implementation of Windsor Container.
    /// </summary>
    public class ProSpecContainer : WindsorContainer
    {
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSpecContainer"/> class.
        /// </summary>
        public ProSpecContainer() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            XmlInterpreter interpreter = new XmlInterpreter();
            interpreter.ProcessResource(interpreter.Source, this.Kernel.ConfigurationStore);
            this.RunInstaller();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSpecContainer"/> class.
        /// </summary>
        /// <param name="interpreter">The interpreter.</param>
        public ProSpecContainer(IConfigurationInterpreter interpreter) : base(interpreter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProSpecContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent container.</param>
        public ProSpecContainer(IWindsorContainer parent) 
            : base(parent, new XmlInterpreter(new StaticContentResource("<configuration/>")))
        {
        }

        /// <summary>
        /// Resolves a service using the specified key.
        /// </summary>
        /// <typeparam name="T">Type of the service.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>Returns an instance of the service.</returns>
        public override T Resolve<T>(string key)
        {
            AssertNotDisposed();
            return base.Resolve<T>(key);
        }

        /// <summary>
        /// Resolves a service using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Returns an instance of the service.</returns>
        public override object Resolve(string key)
        {
            AssertNotDisposed();
            return base.Resolve(key);
        }

        /// <summary>
        /// Resolves a service using the specified key and service type.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service type.</param>
        /// <returns>Returns an instance of the service.</returns>
        public override object Resolve(string key, Type service)
        {
            AssertNotDisposed();
            return base.Resolve(key, service);
        }

        /// <summary>
        /// Resolves a service using the service type.
        /// </summary>
        /// <param name="service">Service type.</param>
        /// <returns>Returns an instance of the service.</returns>
        public override object Resolve(Type service)
        {
            AssertNotDisposed();
            return base.Resolve(service);
        }

        /// <summary>
        /// Asserts this instance is not disposed.
        /// </summary>
        private void AssertNotDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException("The container has been disposed!");
        }

        /// <summary>
        /// Executes Dispose on underlying <see cref="T:Castle.MicroKernel.IKernel"/>
        /// </summary>
        public override void Dispose()
        {
            isDisposed = true;
            base.Dispose();
        }
    }
}
