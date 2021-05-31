using EventSourcingDemo.Core.Models;

namespace EventSourcingDemo.Core.EventBus
{
    public interface IEventConsumerFactory
    {
        IEventConsumer Build<TA, TKey>() where TA : IAggregateRoot<TKey>;
    }
}