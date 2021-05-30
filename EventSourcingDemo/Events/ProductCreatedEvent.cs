using System.Threading;
using System.Threading.Tasks;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Extensions;
using EventSourcingDemo.FakeServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcingDemo.Events
{
    public class ProductCreatedEvent : INotification
    {
        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; set; }
    }

    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly FakeReadDatabase _database;
        private readonly ILogger _logger;

        public ProductCreatedEventHandler(
            FakeReadDatabase database,
            ILogger<ProductCreatedEventHandler> logger
            )
        {
            _database = database;
            _logger = logger;
        }

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _database.SaveData(notification.Product);
            _logger.LogInformation("Product created event >> {json}", notification.Product.ToJson());

            return Task.CompletedTask;
        }
    }
}
