using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface IUserService
    {
        void CreateUser(User user); //Done
        List<User> GetAllUsers(); //Done
        User GetUser(string userId);
        void EditUser(User user); //Done
        void DeleteUser(string userId); //Done
        User ValidteUser(string email, string password); //Done
    }
}
