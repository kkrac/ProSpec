using ProSpec.Acceptance;
using ProSpec.Acceptance.UI.Web;
using Sample.Domain.Model.User;

namespace Sample.Acceptance.Steps
{
    public abstract class SampleStepsContext : WebStepsContext
    {
        private const string CURRENT_USER = "$__Current_User";

        protected ISampleUser CurrentUser
        {
            get { return Get<ISampleUser>(CURRENT_USER, ObjectLifeSpan.Scenario); }
            set { Set<ISampleUser>(CURRENT_USER, value, ObjectLifeSpan.Scenario); }
        }
    }
}
