using System;
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
    public class CreateProductCommand : IRequest<Unit>
    {
        public CreateProductDto Product { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly FakeWriteDatabase _database;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreateProductCommandHandler(
            FakeWriteDatabase database,
            IMapper mapper,
            IMediator mediator,
            ILogger<CreateProductCommandHandler> logger
            )
        {
            _database = database;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var existingData = _database.GetData<Product>(w => (w as Product).Name == request.Product.Name);
            if (existingData != null)
            {
                throw new ArgumentException("Item already exists");
            }

            var data = _mapper.Map<Product>(request.Product);
            _database.SaveData(data);
            _logger.LogInformation("Product created >> {product}", request.Product.ToJson());
            _mediator.Publish(new ProductCreatedEvent(data), cancellationToken);

            return Unit.Task;
        }
    }
}
