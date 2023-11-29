using FashionHexa.Entities;
using FashionHexa.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace FashionHexa.Services
{
    public class RoleService:IRoleService
    {
        private readonly MyContext context=null;

        public RoleService(MyContext Context)
        {
            context = Context;
        }

        public void AddRole(Role role)
        {
            try
            {

                context.Roles.Add(role);
                context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Role> GetAllRoles()
        {
            try
            {
                return context.Roles.ToList();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Role> GetRoleByName(string roleName)
        {
            try
            {
                List<Role> roles= context.Roles.Where(r => r.RoleName == roleName).ToList();

                return roles;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void UpdateRole(Role role)
        {

            try
            {
                if (role != null)
                {
                    role.RoleName = role.RoleName;
                    context.SaveChanges();

                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
