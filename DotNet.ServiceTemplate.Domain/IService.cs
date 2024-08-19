using System.Linq.Expressions;

namespace DotNet.ServiceTemplate.Domain;

public interface IService<T> 
{
    IEnumerable<T> GetAll();
    IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    T Post(T entity);
    T Put(int id, T entity);
    T Delete(int id);
}
