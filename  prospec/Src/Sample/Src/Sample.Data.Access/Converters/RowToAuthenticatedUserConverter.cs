using System;
using System.Data;
using TwoK.Core;
using Sample.Domain.Model;
using Sample.Domain.Model.User;

namespace Sample.Data.Access.Converters
{
    public class RowToAuthenticatedUserConverter : IConverter<IDataReader, AuthenticatedUser>
    {
        public AuthenticatedUser Convert(IDataReader source)
        {
            if (source.Read())
            {
                return new AuthenticatedUser(
                                                (string)source["User.Id"],
                                                (string)source["User.FirstName"],
                                                (string)source["User.LastName"],
                                                (DateTime)source["User.DateOfBirth"],
                                                new Email(source["User.Email"].ToString())
                                            );
            }

            return null;
        }
    }
}
