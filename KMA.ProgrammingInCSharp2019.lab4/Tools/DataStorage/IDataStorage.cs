

using System.Collections.Generic;
using KMA.ProgrammingInCSharp2019.lab4.Models;

namespace KMA.ProgrammingInCSharp2019.lab4.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool UserExists(string email);

        User GetUserByEmail(string email);

        User GetUserByFirstName(string firstname);

        void AddUser(User user);
        void DeleteUser(User user);
        List<User> UsersList { get; }
    }
}
