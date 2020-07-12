using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ISTask.Authentication
{
    public class LocalAccountManager : IAuthStorage
    {
        string pathToStorage { get; }
        XDocument xDoc { get; }
        XElement users => xDoc.Element("Users");

        public LocalAccountManager(string pathToStorage)
        {
            this.pathToStorage = pathToStorage;
            xDoc = new XDocument();
            xDoc = XDocument.Load(pathToStorage);
        }

        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
        }

        public bool Register(string name, string password, Role role)
        {
            try
            {
                var userName = new XAttribute("Name", name);
                var userPassword = new XAttribute("Password", password);
                var userRole = new XAttribute("Role", role);
                users?.Add(new XElement("User", userName, userPassword, userRole));
                users?.Save(pathToStorage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VerifyPassword(string name, string password)
        {
            try
            {
                var user = users
                    ?.Elements("User")
                    ?.Where(x => x.Attribute("Name").Value == name && x.Attribute("Password").Value == password)
                    .FirstOrDefault();
                return user == null ? false : true;
            }
            catch
            {
                return false;
            }
        }

        public Role? GetRole(string name)
        {
            try
            {
                var role = users
                    ?.Elements("User")
                    ?.Where(x => x.Attribute("Name").Value == name)
                    ?.FirstOrDefault()
                    ?.Attribute("Role")
                    ?.Value;
                if (Enum.TryParse(role, out Role parsedRole))
                {
                    return parsedRole;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
    }
}
