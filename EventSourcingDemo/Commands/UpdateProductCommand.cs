using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Dtos;
using EventSourcingDemo.Events;
using EventSourcingDemo.Extensions;
using EventSourcingDemo.FakeServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventSourcingDemo.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public ProductDto Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly FakeWriteDatabase _database;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public UpdateProductCommandHandler(
            FakeWriteDatabase database,
            IMapper mapper,
            IMediator mediator,
            ILogger<UpdateProductCommandHandler> logger
            )
        {
            _database = database;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Product>(request.Product);
            _database.SaveData(data);
            _logger.LogInformation("Product updated >> {product}", request.Product.ToJson());

            _mediator.Publish(new ProductUpdatedEvent(data), cancellationToken);

            return Unit.Task;
        }
    }
}
