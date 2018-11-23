using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSystem
{
    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public override bool Equals(object obj)
        {
            var other = obj as User;
            return this.Login == other.Login && this.Password == other.Password;
        }

        public override int GetHashCode()
        {
            return this.Login.GetHashCode() + this.Password.GetHashCode();
        }
    }
}
