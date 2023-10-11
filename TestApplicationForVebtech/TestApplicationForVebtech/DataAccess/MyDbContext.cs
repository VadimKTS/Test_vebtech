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
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity<RoleUser>();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Users)
                .UsingEntity<RoleUser>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            string[] RoleNames = new[] { "User", "Admin", "Support", "SuperAdmin" };

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
            modelBuilder.Entity<RoleUser>().HasData(
                new RoleUser
                { 
                    RoleId = 1,
                    UserId = 1,
                },
                new RoleUser
                {
                    RoleId = 2,
                    UserId = 1,
                },
                new RoleUser
                {
                    RoleId = 3,
                    UserId = 1,
                },
                new RoleUser
                {
                    RoleId = 4,
                    UserId = 1,
                }
                );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
    }
}
