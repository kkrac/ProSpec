using Krac.Framework.Core.IoC;

namespace Krac.Framework.Acceptance
{
    public abstract class StepsBase<T>
    {
        protected StepsBase()
        {
            Steps = IoCProvider.Resolve<T>();
        }

        protected T Steps { get; private set; }
    }
}
