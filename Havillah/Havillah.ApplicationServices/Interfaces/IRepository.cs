using System.Linq.Expressions;

namespace Havillah.ApplicationServices.Interfaces;

public interface IRepository<T> where T : class
{
    Task Add(T model);
    Task AddRange(IEnumerable<T> models);
    void Update(T model);
    void UpdateRange(IEnumerable<T> models);
    void Delete(T model);
    void DeleteRange(IEnumerable<T> models);
    Task<T> Find(Expression<Func<T, bool>> predicate);
    Task<T> Get(T id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
    Task<int> Save();
}