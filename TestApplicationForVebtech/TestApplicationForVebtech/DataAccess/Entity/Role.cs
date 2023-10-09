﻿namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Guid? UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
