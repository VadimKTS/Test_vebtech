using TestApplicationForVebtech.Services.Interfaces;
using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;
using System.Collections.Generic;

namespace TestApplicationForVebtech.Services.Service
{
    public class RoleUserService : ServiceConstructor, IRoleUserService
    {
        public RoleUserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IList<RoleUser>> GetAllRoleUsersAsync()
        {
            IList<RoleUser> roleUsers = await UnitOfWork.RoleUsers.GetAllAsync();
            return roleUsers;
        }

        //public async Task<IList<RoleUser>> GetRoleUserByUserIdAsync(int id)
        //{
        //    IList<RoleUser> roleUser = await UnitOfWork.RoleUsers.ReadAsync(id);
        //    return roleUser;
        //}

        //public async Task UpdateRoleForUserAsync(User user)
        //{
        //    //!!!!!!!!!!!!!!!!!!!!!!!!
        //}

        public async Task<RoleUser> CreateRoleUserAsync(RoleUser roleUser)
        {
            return await UnitOfWork.RoleUsers.CreateAsync(roleUser);
        }

        public async Task UpdateRoleUserAsync(RoleUser roleUser)
        {
            await UnitOfWork.RoleUsers.UpdateAsync(roleUser);
        }

        public async Task DeleteRoleUserAsync(RoleUser roleUser)
        {
            await UnitOfWork.RoleUsers.UpdateAsync(roleUser);
        }
    }
}
