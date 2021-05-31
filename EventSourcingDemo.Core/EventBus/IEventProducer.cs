using System.Threading.Tasks;
using EventSourcingDemo.Core.Models;

namespace EventSourcingDemo.Core.EventBus
{
    public interface IEventProducer<in TA, in TKey>
        where TA : IAggregateRoot<TKey>
    {
        Task DispatchAsync(TA aggregateRoot);
    }
}