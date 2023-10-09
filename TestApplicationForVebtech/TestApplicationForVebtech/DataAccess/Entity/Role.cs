namespace TestApplicationForVebtech.DataAccess.Entity
{
    public class Role
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<UserRoles>? Roles { get; set; }//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Исправить логику ролей
    }
}
