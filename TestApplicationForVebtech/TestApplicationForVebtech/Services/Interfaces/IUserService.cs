using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task UpdateRoleForUserAsync(User user);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(Guid id);
        Task DeleteUserAsync(Guid id);
    }
}
