using ProSpec.Acceptance.UI.Web;
using Sample.Domain.Model.User;

namespace Sample.Acceptance.Steps
{
    public abstract class SampleStepsContext : WebStepsContext
    {
        protected ISampleUser CurrentUser
        {
            get { return Get<ISampleUser>(); }
            set { Set<ISampleUser>(value); }
        }
    }
}