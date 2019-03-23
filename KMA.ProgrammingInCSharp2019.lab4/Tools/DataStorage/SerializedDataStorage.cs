
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KMA.ProgrammingInCSharp2019.lab4.Managers;
using KMA.ProgrammingInCSharp2019.lab4.Models;

namespace KMA.ProgrammingInCSharp2019.lab4.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<User> _users;


        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<User>();
            }
        }

        public bool UserExists(string email)
        {
            return _users.Exists(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByFirstName(string firstname)
        {
            return _users.FirstOrDefault(u =>u.Name == firstname);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _users.Remove(user);
            SaveChanges();
        }

        public List<User> UsersList
        {
            get { return _users.ToList(); }
        }

        internal void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

    }
}
