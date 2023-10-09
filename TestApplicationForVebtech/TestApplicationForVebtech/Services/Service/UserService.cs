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
            IList<User> users = await UnitOfWork.Users.ReadAsync();
        }
        //Task<User> GetUserByIdAsync(Guid id);
        //Task UpdateRoleForUserAsync(User user);
        //Task<User> CreateUserAsync(User user);
        //Task<User> UpdateUserAsync(Guid id);
        //Task DeleteUserAsync(Guid id);
    }
}
