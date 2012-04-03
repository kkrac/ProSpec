using System.Collections;
using TechTalk.SpecFlow;

namespace Krac.Framework.Acceptance
{
    public class TestsContext : SpecFlowContext
    {
        private static TestsContext current;
        public static TestsContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new TestsContext();
                }

                return current;
            }
        }
    }
}
