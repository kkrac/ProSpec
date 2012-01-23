using TwoK.Core;
using TwoK.Core.Benchmarking;
using Sample.Data.Access.Queries;

namespace Sample.Data.Access
{
    public class DbBenchmarkWriter : IBenchmarkWriter
    {
        public DbBenchmarkWriter(IApplicationSession session, IBenchmark_Insert insertBenchmark)
        {
            this.session = session;
            this.insertBenchmark = insertBenchmark;
        }

        private IApplicationSession session;
        private IBenchmark_Insert insertBenchmark;

        public void Write(Benchmark benchmark)
        {
            string arguments = string.Empty;

            for (int i = 0; i < benchmark.Arguments.Length; i++)
            {
                if (!string.IsNullOrEmpty(arguments))
                {
                    arguments = string.Format("{0}, ", arguments);
                }

                arguments = string.Format("{0}{1}", arguments, benchmark.Arguments[i]);
            }

            insertBenchmark.Execute(benchmark.Target.FullName, benchmark.Method.Name, arguments,
                benchmark.ElapsedMilliseconds, benchmark.TimeStamp, session.User == null ? "dbo" : session.User.Id);
            //TODO2: session.User.Id);
        }
    }
}
