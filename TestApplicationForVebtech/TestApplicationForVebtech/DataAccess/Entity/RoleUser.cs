using System.Text.Json.Serialization;

namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class RoleUser
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; } = null!;
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
