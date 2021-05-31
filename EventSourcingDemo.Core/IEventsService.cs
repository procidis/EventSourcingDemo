using System.Threading.Tasks;
using EventSourcingDemo.Core.Models;

namespace EventSourcingDemo.Core
{
    public interface IEventsService<TA, TKey> 
        where TA : class, IAggregateRoot<TKey>
    {
        Task PersistAsync(TA aggregateRoot);
        Task<TA> RehydrateAsync(TKey key);
    }
}