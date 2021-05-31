namespace EventSourcingDemo.Core.Models
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}