using Microsoft.EntityFrameworkCore;
using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
