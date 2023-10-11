using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
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
        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserByIdPost(int id)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        [HttpPost("RegistrationUser")]
        public async Task<IActionResult> RegistrationUserPost(RegistrationUserViewModel registrationModel)
        {
            var allUsers = (List<User>)await _userService.GetAllUsersAsync();
            var userWhithSameEmail = allUsers.FirstOrDefault(user => user.Email == registrationModel.Email);
            if (userWhithSameEmail != null)
            {
                return BadRequest($"Пользователь с таким Email ({registrationModel.Email}) уже зарегистрирован!");
            }
            var userRole = await _roleService.GetRoleByIdAsync(1);
            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Name = registrationModel.Name,
                    Age = registrationModel.Age,
                    Email = registrationModel.Email,
                    Password = registrationModel.Password,
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost("AddRoleForUser")]
        public async Task<IActionResult> AddRoleForUserPost(int userId, int roleId)
        {
            var user = await _userService.GetUserByIdAsync(userId); 
            var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();

            if (user == null)
            {
                return NotFound($"Пользователь с ID={userId} не найден.\nError: response status is 404");
            }
            else if (user.Roles.FirstOrDefault(r => r.Id.Equals(roleId)) == null) 
            {
                var role = await _roleService.GetRoleByIdAsync(roleId);
                if (role == null) 
                {
                    return NotFound($"Роль с ID={roleId} не найдена.\nError: response status is 404");
                }
                user.Roles.Add(role);
                await _userService.UpdateUserAsync(user);
                return Ok($"Пользователю \"{user.Name}\" добавлена роль \"{role.Name}\".");
            }
            else
            {
                return Ok($"У пользователя \"{user.Name}\" уже имеется роль с Id={roleId}.");
            }
        }


    }
}
