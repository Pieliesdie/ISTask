using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTask.Authentication
{
    public interface IAuthStorage
    {
        bool Register(string login, string password, Role role);

        bool VerifyPassword(string login, string password);

        void DeleteUser(string login);

        Role? GetRole(string login);
    }
}
