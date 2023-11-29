using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface IRoleService
    {
        void AddRole(Role role);
        List<Role> GetAllRoles();
        List<Role> GetRoleByName(string roleName);
        void UpdateRole(Role role);
    }
}
