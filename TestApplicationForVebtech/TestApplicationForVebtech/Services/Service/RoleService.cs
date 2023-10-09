using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Services.Interfaces;

namespace TestApplicationForVebtech.Services.Service
{
    public class RoleService : ServiceConstructor, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddNewRoleForUserAsync(User user, Role role)
        {
            if (role != null)
            {
                role.Users.Add(user);
                await UnitOfWork.Roles.UpdateAsync(role);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            else
            {
                throw new InvalidOperationException(); //исправить!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }            
        }

        public async Task<Role> GetRoleForUserAsync(int userId)
        {
            var userRole = await UnitOfWork.Roles.ReadAsync(userId);
            return userRole;
        }
    }
}
