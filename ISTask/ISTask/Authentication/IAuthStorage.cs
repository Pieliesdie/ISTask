using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTask.Authentication
{
    public interface IAuthStorage
    {
        bool Register(string name, string password, Role role);

        bool VerifyPassword(string name, string password);

        void DeleteUser(string name);

        Role? GetRole(string name);
    }
}
