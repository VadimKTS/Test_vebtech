using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Interfaces
{
    public interface IRoleUserService
    {
        Task<IList<RoleUser>> GetAllRoleUsersAsync();
        //Task<IList<RoleUser>> GetRoleUserByUserIdAsync(int id);
        //Task UpdateRoleForUserAsync(User user);
        Task<RoleUser> CreateRoleUserAsync(RoleUser roleUser);
        Task UpdateRoleUserAsync(RoleUser roleUser);
        Task DeleteRoleUserAsync(RoleUser roleUser);
    }
}
