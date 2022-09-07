using Final330.Models;
using System.Collections.Generic;

namespace Final330
{
    public interface IUserRepo
    {
        IEnumerable<User> Users { get; }

        void Add(User user);
        bool Update(int userId, User user);
        bool Delete(int userId);
    }
    public class UsersRepo : IUserRepo
    {
        private static List<User> users = new List<User>();
        private static int currentId = 101;

        public IEnumerable<User> Users
        {
            get
            {
                return users;
            }
        }
        public void Add(User user)
        {
            user.Id = currentId++;
            user.DateAdded = DateTime.Now;

            users.Add(user);
        }

        public bool Delete(int userId)
        {
            var rowsDeleted = users.RemoveAll(t => t.Id == userId);
            
            return (rowsDeleted > 0);
        }

        public bool Update(int userId, User updatedUser)
        {
            var user = users.FirstOrDefault(t => t.Id == userId);

            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
