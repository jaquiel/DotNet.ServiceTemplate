using System.Linq.Expressions;

namespace DotNet.ServiceTemplate.Infrastructure;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> Get(Expression<Func<T, bool>>? filter);
    T Add(T entity);
    T Update(int id, T entity);
    T Remove(T entity);


    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
}

public interface IAdvancedRepository<T> : IRepository<T> where T : class
{
    IEnumerable<T> Get(int skip, int take);
}