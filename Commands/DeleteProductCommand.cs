using System.Threading;
using System.Threading.Tasks;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Events;
using EventSourcingDemo.FakeServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcingDemo.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly FakeWriteDatabase _database;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public DeleteProductCommandHandler(
            FakeWriteDatabase database,
            IMediator mediator,
            ILogger<DeleteProductCommandHandler> logger
            )
        {
            _database = database;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (_database.DeleteData<Product>(request.Id))
            {
                _logger.LogInformation("Product deleted >> {id}", request.Id);
                _mediator.Publish(new ProductDeletedEvent(request.Id), cancellationToken);
            }
            return Unit.Task;
        }
    }
}
