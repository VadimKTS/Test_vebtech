using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        //Task UpdateRoleForUserAsync(User user);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
