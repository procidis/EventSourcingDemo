using System.Threading;
using System.Threading.Tasks;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Extensions;
using EventSourcingDemo.FakeServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcingDemo.Events
{
    public class ProductUpdatedEvent : INotification
    {
        public ProductUpdatedEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; set; }
    }

    public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        private readonly FakeReadDatabase _database;
        private readonly ILogger _logger;

        public ProductUpdatedEventHandler(
            FakeReadDatabase database,
            ILogger<ProductUpdatedEventHandler> logger
            )
        {
            _database = database;
            _logger = logger;
        }

        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _database.SaveData(notification.Product);
            _logger.LogInformation("Product updated event >> {product}", notification.Product.ToJson());

            return Task.CompletedTask;
        }
    }
}
