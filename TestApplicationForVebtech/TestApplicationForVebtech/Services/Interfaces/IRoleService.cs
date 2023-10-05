using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Interfaces
{
    public interface IRoleService
    {
        Task AddNewRoleForUserAsync(Role role, UserRoles userRole);
    }
}
