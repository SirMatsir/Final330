using Final330.Models;

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
        public List<User> usersList = new List<User>() 
        { 
            new User() { Id = 1, Name = "Philip", DateAdded = DateTime.Now },
            new User() { Id = 2, Name = "Abdul", DateAdded = DateTime.Now},
            new User() { Id = 3, Name = "Normand", DateAdded = DateTime.Now},
            new User() { Id = 4, Name = "Horatio", DateAdded = DateTime.Now}
        };
        public IEnumerable<User> Users
        {
            get
            {
                var users = usersList;
                var items = users.Select(t => new User
                {
                    Id = t.Id,
                    Name = t.Name,
                    DateAdded = t.DateAdded,
                }).ToList();

                return items;
            }
        }

        public void Add(User user)
        {
            try
            {
                var newUser = new User
                {
                    Name = user.Name,
                };
                usersList.Add(newUser);

                user.Id = newUser.Id;
                user.Name = newUser.Name;
                user.DateAdded = newUser.DateAdded;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public bool Delete(int userId)
        {
            var user = usersList.FirstOrDefault(t => t.Id == userId);

            if (user == null)
            {
                return false;
            }
            usersList.Remove(user);

            return true;
        }

        public bool Update(int userId, User updatedUser)
        {
            var listUser = usersList.FirstOrDefault(t => t.Id == userId);

            if (listUser == null)
            {
                return false;
            }
            listUser.Name = updatedUser.Name;

            updatedUser.Id = listUser.Id;
            updatedUser.Name = listUser.Name;
            updatedUser.DateAdded = listUser.DateAdded;

            return true;
        }
    }
}
