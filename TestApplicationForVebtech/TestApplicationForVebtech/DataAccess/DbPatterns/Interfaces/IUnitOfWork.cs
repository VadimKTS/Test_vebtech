using TestApplicationForVebtech.DataAccess.Entity;

namespace TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Role> Roles { get; }
    }
}
