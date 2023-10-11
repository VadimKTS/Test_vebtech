using TestApplicationForVebtech.Services.Interfaces;
using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Services.Service
{
    public class UserService : ServiceConstructor, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            IList<User> users = await UnitOfWork.Users.GetAllAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            User user = await UnitOfWork.Users.ReadAsync(id);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await UnitOfWork.Users.CreateAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await UnitOfWork.Users.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await UnitOfWork.Users.DeleteAsync(user);
        }
    }
}
