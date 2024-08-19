using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotNet.ServiceTemplate.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _context;

    public Repository(DataContext context)
    {
        _context = context;
    }

    public T Add(T entity)
    {
        var result = _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return result.Entity;
       
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).AsQueryable();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().Where(filter.Compile()).AsQueryable();
    }

    public T Remove(T entity)
    {
        var result = _context.Set<T>().Remove(entity);   
        
        return result.Entity;
    }

    public T Update(int id, T entity)
    {
        var result = _context.Set<T>().Update(entity);

        return result.Entity;
    }
}
