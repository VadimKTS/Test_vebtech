using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<RoleUser> RoleUsers { get; set; } = new();
        [JsonIgnore]
        public List<User> Users { get; set; } = new();
    }
}
