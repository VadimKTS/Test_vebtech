﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public List<RoleUser> RoleUsers { get; set; } = new ();
        [JsonIgnore]
        public List<Role> Roles { get; set; } = new ();
    }
}
