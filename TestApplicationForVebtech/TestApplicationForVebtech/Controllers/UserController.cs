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
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<UserViewModel>> GetAllUsersGet()
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
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserByIdGet(int id)
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
                return Ok(userModel);
            }
            return NotFound($"Пользователь с ID={id} не найден.\nError: response status is 404");
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel registerModel)
        {
            //var allUsers = (List<User>)await _userService.GetAllUsersAsync();
            //var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();
            //var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();

            var userRole = await _roleService.GetRoleByIdAsync(1);
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Name = registerModel.Name,
                    Age = registerModel.Age,
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                };
                await _userService.CreateUserAsync(newUser);
                var newRoleUser = new RoleUser()
                {
                    RoleId = userRole.Id,
                    Role = userRole,
                    UserId = newUser.Id,
                    User = newUser,
                };
                newUser.RoleUsers = new List<RoleUser>() { newRoleUser };
                userRole.Users.Add(newUser);
                await _userService.UpdateUserAsync(newUser);
                return Ok(newUser);
            }
            else
            {
                return BadRequest("Модель не валидна");
            }
        }
    }
}
