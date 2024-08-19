using DotNet.ServiceTemplate.Infrastructure;
using System.Linq.Expressions;

namespace DotNet.ServiceTemplate.Domain
{
    public class Service<T> : IService<T>
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter)
        {
            var repo = _repository.Get(filter);

            return repo;
        }

        public IEnumerable<T> GetAll()
        {
            var result = _repository.GetAll().AsEnumerable();

            return result;
        }

        public T Post(T entity)
        {
            return _repository.Add(entity);
        }

        public T Put(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
