using System.Linq.Expressions;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Havillah.Persistense.Repository;

public class Repository<T>: IRepository<T> where T: class
{
    private readonly DatabaseContext _context;
    public Repository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task Add(T model)
    {
        await _context.Set<T>().AddAsync(model);
    }

    public async Task AddRange(IEnumerable<T> models)
    {
        await _context.Set<T>().AddRangeAsync(models);
    }

    public void Update(T model)
    {
        _context.Set<T>().Update(model);
    }

    public void UpdateRange(IEnumerable<T> models)
    {
        _context.Set<T>().UpdateRange(models);
    }

    public void Delete(T model)
    {
      _context.Set<T>().Remove(model);
    }
    
    public void DeleteRange(IEnumerable<T> models)
    { 
        _context.Set<T>().RemoveRange(models);
    }

    public async Task<T> Find(Expression<Func<T, bool>> predicate)
        => (await _context.Set<T>().FirstOrDefaultAsync(predicate))!;

    public async Task<T> Get(T id)
        => (await _context.Set<T>().FindAsync(id))!;

    public async Task<IEnumerable<T>> GetAll()
        => (await _context.Set<T>().ToListAsync())!;

    public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        => (await _context.Set<T>().Where(predicate).ToListAsync())!;

    public async Task<int> Save()
    {
        var result = await _context.SaveChangesAsync(new CancellationToken());
        return result;
    }
}