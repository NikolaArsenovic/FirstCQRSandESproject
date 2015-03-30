using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ES.Domain
{
    public interface IRepository<T> where T: AggregateRoot,new() //repository interface
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
    public class Repository<T>:IRepository<T> where T:AggregateRoot,new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.id, aggregate.GetUncommittedChanges(), expectedVersion);
           
        }

      
        public T GetById(Guid id)
        {
            var obj = new T();
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}
