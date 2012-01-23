using System;
using System.Data;
using Sample.Data.Access.Queries;
using Sample.Domain.Model.User;
using TwoK.Core;
using TwoK.Core.Security.Encryption;
using TwoK.Data.Access;

namespace Sample.Data.Access.Implementations
{
    public class UserAccountDAO : AbstractDAO, IUserAccountDAO
    {
        public UserAccountDAO(IRepository<IQuery> queries, IApplicationSession session) : base(queries)
        {
            this.session = session;
        }

        private IApplicationSession session;

        public string GetPasswordForUser(string userId)
        {
            IUser_SelectPassword query = Queries.Find<IUser_SelectPassword>();
            string password = string.Empty;

            using (IDataReader reader = query.Execute(userId))
            {
                if (reader.Read())
                {
                    password = reader["Password"] != null ? reader["Password"].ToString() : string.Empty;
                }
            }

            return password;
        }

        public string CreateAccount(ISampleUser user, string password)
        {
            IPerson_Insert insertPerson = Queries.Find<IPerson_Insert>();
            
            int personId =  (int)insertPerson.Execute
                                (
                                    user.FirstName,
                                    user.LastName,
                                    user.Email.ToString(),
                                    user.DateOfBirth,
                                    user.Id,
                                    DateTime.Now
                                );

            IUser_Insert insertUser = Queries.Find<IUser_Insert>();

                                insertUser.Execute
                                (
                                    user.Id,
                                    personId,
                                    password,
                                    false,
                                    user.Id,
                                    DateTime.Now
                                );

            string token = PasswordGenerator.Generate(8);

            IUser_Temp_Insert insertTemporaryAccount = Queries.Find<IUser_Temp_Insert>();

                                insertTemporaryAccount.Execute
                                (
                                    user.Id,
                                    token
                                );

            return token;
        }

        public ISampleUser GetUser(string userId)
        {
            IUser_SelectById selectUser = Queries.Find<IUser_SelectById>();

            return selectUser.Execute(userId);
        }

        public string GetTokenForUser(string userId)
        {
            IUser_Temp_Token_Select selectToken = Queries.Find<IUser_Temp_Token_Select>();

            string token = string.Empty;

            using (IDataReader reader = selectToken.Execute(userId))
            {
                if (reader.Read())
                {
                    token = reader["Token"].ToString();
                }
            }

            return token;
        }

        public void ActivateAccount(string userId)
        {
            IUser_Activate activateUser = Queries.Find<IUser_Activate>();
            activateUser.Execute(userId);

            IUser_Temp_Delete deleteTemporaryUser = Queries.Find<IUser_Temp_Delete>();
            deleteTemporaryUser.Execute(userId);
        }
    }
}