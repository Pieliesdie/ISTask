using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTask.Authentication
{
    public class LocalAccountManager : IAuthStorage
    {
        string pathToStorage { get; set; }

        public LocalAccountManager(string pathToStorage)
        {
            this.pathToStorage = pathToStorage;
        }

        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
        }

        public bool Register(string name, string password, Role role)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
