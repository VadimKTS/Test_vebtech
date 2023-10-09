using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Interfaces
{
    public interface IRoleService
    {
        Task AddNewRoleForUserAsync(User user, Role role);
        Task<Role> GetRoleForUserAsync(int userId);
    }
}
