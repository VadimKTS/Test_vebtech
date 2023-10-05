using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;

namespace TestApplicationForVebtech.Services.Service
{
    public class ServiceConstructor
    {
        protected IUnitOfWork UnitOfWork;

        protected ServiceConstructor(IUnitOfWork unitOfWork) 
        { 
            UnitOfWork = unitOfWork;
        }
    }
}
