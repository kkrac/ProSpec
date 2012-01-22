using System;
using TwoK.Core.IoC;
using TwoK.Core.Security.Encryption;
using Sample.Acceptance.Steps.UI;
using Sample.Acceptance.Support;
using Sample.Data.Access;
using Sample.Domain.Model.User;
using TechTalk.SpecFlow;

namespace Sample.Acceptance.Steps
{
    public class UserAccountStepsContext : SampleStepsContext
    {
        [Given(@"the following temporary accounts exist")]
        public void Given_The_Following_Temporary_Account_Exists(Table table)
        {
            int i = 0;

            foreach (var row in table.Rows)
            {
                string userName = row["UserName"];
                string token = row["Token"];

                UserAccountStepsUtils.CreateAccount(userName, "P@ssword1", token, "john" + i, "doe", "john" + i + "@doe.com", DateTime.Now);
            }
        }

        [Given(@"the following users exist")]
        public void Given_The_Following_Users_Exist(Table table)
        {
            IUserAccountDAO accountDAO = IoCProvider.Resolve<IUserAccountDAO>();
            IEncryptionService encryptionService = IoCProvider.Resolve<IEncryptionService>();
            string encryptedPassword;

            foreach (var row in table.Rows)
            {
                var user = new AuthenticatedUser
                (
                    row["UserName"],
                    row["FirstName"],
                    row["LastName"],
                    Convert.ToDateTime(row["DateOfBirth"]),
                    row["Email"]
                );

                encryptedPassword = encryptionService.Encrypt(row["Password"], user.Id);
                accountDAO.CreateAccount(user, encryptedPassword);
            }
        }
    }
}
