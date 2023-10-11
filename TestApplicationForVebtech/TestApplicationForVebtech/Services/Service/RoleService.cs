using System.Data;
using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Services.Interfaces;

namespace TestApplicationForVebtech.Services.Service
{
    public class RoleService : ServiceConstructor, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IList<Role>> GetAllRolesAsync()
        {
            IList<Role> users = await UnitOfWork.Roles.GetAllAsync();
            return users;
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            Role user = await UnitOfWork.Roles.ReadAsync(id);
            return user;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            return await UnitOfWork.Roles.CreateAsync(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            await UnitOfWork.Roles.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(Role role)
        {
            await UnitOfWork.Roles.DeleteAsync(role);
        }
    }
}
