using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Services.Interfaces;

namespace TestApplicationForVebtech.Services.Service
{
    public class RoleService : ServiceConstructor, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddNewRoleForUserAsync(Role role, UserRoles userRole)
        {
            if (role != null)
            {
                role.Roles.Add(userRole);
                await UnitOfWork.Roles.UpdateAsync(role);
            }
            else
            {
                throw new InvalidOperationException(); //исправить!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            
        }
    }
}
