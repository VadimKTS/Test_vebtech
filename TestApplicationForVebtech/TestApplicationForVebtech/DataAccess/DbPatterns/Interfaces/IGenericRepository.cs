namespace TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> CreateAsync(T t);
        Task<IList<T>> GetAllAsync();
        Task<T> ReadAsync(int id);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);

    }
}
