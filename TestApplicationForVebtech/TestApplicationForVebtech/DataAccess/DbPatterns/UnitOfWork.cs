using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;
using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.DataAccess.DbPatterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _DbContext;

        public UnitOfWork(MyDbContext myDbContext)
        {
            _DbContext = myDbContext;
        }

        public IGenericRepository<User> Users => new GenericRepository<User>(_DbContext);
        public IGenericRepository<Role> Roles => new GenericRepository<Role>(_DbContext);
        public IGenericRepository<UserRole> UserRoles => new GenericRepository<UserRole>(_DbContext);
    }
}
