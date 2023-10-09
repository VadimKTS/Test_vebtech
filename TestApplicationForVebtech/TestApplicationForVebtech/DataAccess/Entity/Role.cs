using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleUser> RoleUsers { get; set; } = new();
        public List<User> Users { get; set; } = new();
    }
}
