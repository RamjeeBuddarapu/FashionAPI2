using FashionHexa.Entities;
using FashionHexa.Database;

namespace FashionHexa.Services
{
    public class UserService:IUserService
    {
        private readonly MyContext context = null;
        public UserService(MyContext Context)
        {
            context = Context;
        }
        public void CreateUser(User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(string userId)
        {
            User user = context.Users.Find(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUser(string userId)
        {
            return context.Users.Find(userId);
        }

        public User ValidteUser(string email, string password)
        {
            return context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
