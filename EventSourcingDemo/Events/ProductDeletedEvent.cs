using System.Threading;
using System.Threading.Tasks;
using EventSourcingDemo.Domain;
using EventSourcingDemo.FakeServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcingDemo.Events
{
    public class ProductDeletedEvent : INotification
    {
        public ProductDeletedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

    public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
    {
        private readonly FakeReadDatabase _database;
        private readonly ILogger _logger;

        public ProductDeletedEventHandler(
            FakeReadDatabase database,
            ILogger<ProductDeletedEventHandler> logger
            )
        {
            _database = database;
            _logger = logger;
        }

        public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            _database.DeleteData<Product>(notification.Id);
            _logger.LogInformation("Product deleted event >> {id}", notification.Id);

            return Task.CompletedTask;
        }
    }
}
