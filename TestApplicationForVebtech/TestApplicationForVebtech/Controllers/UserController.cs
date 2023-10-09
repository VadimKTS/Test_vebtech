using Microsoft.AspNetCore.Mvc;
using TestApplicationForVebtech.DataAccess.Entity;
using TestApplicationForVebtech.Models.UserModels;
using TestApplicationForVebtech.Services.Interfaces;

namespace TestApplicationForVebtech.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersInList()
        {
            List<User> allUsers = (List<User>)await _userService.GetAllUsersAsync();
            var usersViewModel = new List<UsersListViewModel>();
            foreach (var item in allUsers)
            {
                Role userRoles = await _roleService.GetRoleForUserAsync(item.Id);
                var newModel = new UsersListViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Age = item.Age,
                    //Roles = userRoles.Roles, !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                };
                usersViewModel.Add(newModel);
            }

            return View(usersViewModel);
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
