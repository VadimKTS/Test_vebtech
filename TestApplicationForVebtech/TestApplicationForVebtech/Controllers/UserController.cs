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
        public async Task<IList<UsersListViewModel>> GetAllUsersInList()
        {
            List<User> allUsers = (List<User>)await _userService.GetAllUsersAsync();
            List<Role> allRoles = (List<Role>)await _roleService.GetAllRolesAsync();
            List<RoleUser> roleUsers = (List<RoleUser>)await _roleUserService.GetAllRoleUsersAsync();
            var usersViewModel = new List<UsersListViewModel>();

            foreach (var user in allUsers)
            {
                var userModel = new UsersListViewModel()
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


        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
