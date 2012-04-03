namespace ProSpec.Core
{
    using TechTalk.SpecFlow;

    public class GlobalContext : SpecFlowContext
    {
        private static GlobalContext current;
        public static GlobalContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new GlobalContext();
                }

                return current;
            }
        }
    }
}
