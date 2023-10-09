using Microsoft.EntityFrameworkCore;
using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string[] RoleNames = new[] { "User", "Admin", "Support", "SuperAdmin" };
            var roles = new List<Role>();

            for (int i = 1; i < RoleNames.Length+1; i++)
            {
                modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = i,
                    Name = RoleNames[i - 1],
                }
                );
            }

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Age = 30,
                    Email = "testUserEmail@mail.ru",
                    Name = "TestUser",
                    Password = "qwerty", //!!!!!!!!!!!!!!добавить Hash
                }
                );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    RolesId = 1,
                    UsersId = 1,
                },
                new UserRole
                {
                    Id = 2,
                    RolesId = 4,
                    UsersId = 1,
                }
                );
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
