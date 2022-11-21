using System.Linq.Expressions;
using Havillah.ApplicationServices.Interfaces;
using Microsoft.VisualBasic;

namespace Havillah.Persistence.Repository;

public class Repository<T>: IRepository<T> where T: class, new()
{
    private readonly DatabaseContext _context;
    public Repository(DatabaseContext context)
    {
        this._context = context;
    }
    
    public async Task Add(T model)
    {
        await _context.Set<T>().AddAsync(model);
    }

    public async Task AddRange(IEnumerable<T> models)
    {
        await _context.Set<T>().AddRangeAsync(models);
    }

    public Task Update(T model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRange(IEnumerable<T> models)
    {
        throw new NotImplementedException();
    }

    public Task Delete(T model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRange(IEnumerable<T> models)
    {
        throw new NotImplementedException();
    }

    public Task<T> Find(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> Get(T id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Save()
    {
        var result = await _context.SaveChangesAsync(new CancellationToken());
        return result;
    }
}