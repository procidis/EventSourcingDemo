using System;

namespace EventSourcingDemo.Core.Models
{
    public interface IDomainEvent<out TKey>
    {
        long AggregateVersion { get; }
        TKey AggregateId { get; }
        DateTime Timestamp { get; }
    }
}