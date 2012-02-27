using System;
using TwoK.Data.Access;

namespace Sample.Data.Access
{
    public class ConnectionStringProvider : IDbConnectionStringService
    {
        public ConnectionStringProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string connectionString;

        public string GetConnectionString()
        {
            return connectionString;
        }

        public void RenewConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
