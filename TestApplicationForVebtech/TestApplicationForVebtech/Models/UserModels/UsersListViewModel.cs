using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.Models.UserModels
{
    public class UsersListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
