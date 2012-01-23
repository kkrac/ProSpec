using System;
using System.Text.RegularExpressions;

namespace Sample.Domain.Model
{
    public class Email
    {
        public Email(string address)
        {
            if (!regEx.IsMatch(address))
            {
                throw new ApplicationException("The e-mail address is invalid");
            }

            int indexOfAt = address.IndexOf('@');
            UserName = address.Substring(0, indexOfAt);
            Domain = address.Substring(indexOfAt + 1);
        }

        private static Regex regEx = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
            + @"[a-zA-Z]{2,}))$");

        public string UserName { get; private set; }
        public string Domain { get; private set; }

        public static implicit operator Email(string address)
        {
            Email email = new Email(address);
            return email;
        }

        public static implicit operator string(Email email)
        {
            return email.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0}@{1}", UserName, Domain);
        }

        public override bool Equals(object obj)
        {
            Email compared = obj as Email;

            if (compared == null)
            {
                return false;
            }

            return ToString().Equals(compared.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
