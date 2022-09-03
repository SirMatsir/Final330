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
            new User() { Id = 24, Name = "Philip", DateAdded = DateTime.Now },
            new User() { Id = 4, Name = "Abdul", DateAdded = DateTime.Now},
            new User() { Id = 5, Name = "Normand", DateAdded = DateTime.Now},
            new User() { Id = 6, Name = "Horatio", DateAdded = DateTime.Now}
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
            throw new NotImplementedException();
        }

        public bool Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int userId, User user)
        {
            throw new NotImplementedException();
        }
    }
}
