using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Models.SortModels;
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
        /// Получение списка всех пользователе с их ролями
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int? ageFilter, string nameFilter = "any", string emailFilter = "any", UserSortState userSortOrder = UserSortState.NameAsc, RoleSortState roleSortOrder = RoleSortState.NameAsc, int page = 1, int pageSize = 3)
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
            //----------------pagination-----------------------
            var totalCount = usersViewModel.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var usersPerPage = usersViewModel
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            if (page > totalPages)
            {
                return NotFound($"Всего страниц {totalPages}. \nОбъектов на странице {pageSize}.");
            }
            //--------------------sort-------------------------
            usersPerPage = userSortOrder switch
            {
                UserSortState.NameDesc => usersPerPage.OrderByDescending(s => s.Name).ToList(),
                UserSortState.AgeAsc => usersPerPage.OrderBy(s => s.Age).ToList(),
                UserSortState.AgeDesc => usersPerPage.OrderByDescending(s => s.Age).ToList(),
                UserSortState.EmailAsc => usersPerPage.OrderBy(s => s.Email).ToList(),
                UserSortState.EmailDesc => usersPerPage.OrderByDescending(s => s.Email).ToList(),
                _ => usersPerPage.OrderBy(s => s.Name).ToList(),
            };

            foreach (var user in usersPerPage)
            {
                user.Roles = roleSortOrder switch
                {
                    RoleSortState.NameDesc => user.Roles.OrderByDescending(s => s.Name).ToList(),
                    _ => user.Roles.OrderBy(s => s.Name).ToList(),
                };
            }
            //--------------------Filter----------------------------
            if (!nameFilter.Equals("any"))
            {
                usersPerPage = usersPerPage.Where(user => user.Name == nameFilter).ToList();
            }
            if (ageFilter != null)
            {
                usersPerPage = usersPerPage.Where(user => user.Age == ageFilter).ToList();
            }
            if (!emailFilter.Equals("any"))
            {
                usersPerPage = usersPerPage.Where(user => user.Email == emailFilter).ToList();
            }

            if (usersPerPage.IsNullOrEmpty())
            {
                return NotFound("Не найдено пользователей соответствующих параметрам фильтрации.");
            }

            return Ok(usersPerPage);
        }

        /// <summary>
        /// Получение пользователя по id и всех его ролей 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
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
        /// Создание нового пользователя
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
                return Ok($"Пользователь с email:{newUser.Email} успешно зарегистрирован.");
            }
            else
            {
                return BadRequest("Модель не валидна");
            }
        }

        /// <summary>
        /// Добавление новой роли пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPut("AddRoleForUser")]
        public async Task<IActionResult> AddRoleForUserPut(int userId, int roleId)
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

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUserById")]
        public async Task<IActionResult> DeleteUserByIdDelete(int userId)
        {
            var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Пользователь с ID={userId} не найден.\nError: response status is 404");
            }
            await _userService.DeleteUserAsync(user);
            return Ok($"Пользователь с ID={userId} удален.");
        }

        /// <summary>
        /// Обновление информации о пользователе по id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("EditUserById")]
        public async Task<IActionResult> EditUserByIdPut(int userId, EditUserViewModel model)
        {
            var roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var allRoles = (List<Role>)await _roleService.GetAllRolesAsync();

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Пользователь с ID={userId} не найден.\nError: response status is 404");
            }

            if (ModelState.IsValid)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Age = model.Age;
                user.Password = model.Password;

                await _userService.UpdateUserAsync(user);
                return Ok(user);
            }
            return BadRequest("Модель не валидна");
        }
    }
}
