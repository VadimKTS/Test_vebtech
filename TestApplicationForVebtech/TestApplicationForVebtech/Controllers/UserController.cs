using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Models.UserModels;
using TestApplicationForVebtech.Services.Interfaces;

namespace TestApplicationForVebtech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IRoleUserService _roleUserService;
        public UserController(IUserService userService, IRoleService roleService, IRoleUserService roleUserService)
        {
            _userService = userService;
            _roleService = roleService;
            _roleUserService = roleUserService;
        }

        /// <summary>
        /// GetAllUsersInList
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllUsersInList")]
        public async Task<IEnumerable<UserViewModel>> GetAllUsersInList()
        {
            var allUsers = (List<User>)await _userService.GetAllUsersAsync();
            var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();
            var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var usersViewModel = new List<UserViewModel>();

            foreach (var user in allUsers)
            {
                var userModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Age = user.Age,
                    Email = user.Email,
                    Roles = user.Roles,
                };
                usersViewModel.Add(userModel);
            }

            return usersViewModel;
        }

        /// <summary>
        /// "GetUserById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetUserById")]
        public async Task<UserViewModel> GetUserById(int id)
        {
            var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                var userModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Age = user.Age,
                    Email = user.Email,
                    Roles = user.Roles,
                };
                return userModel;
            }
            return new UserViewModel();
        }
    }
}
