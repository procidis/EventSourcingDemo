using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Dtos;
using EventSourcingDemo.FakeServices;
using MediatR;

namespace EventSourcingDemo.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public int Id { get; private set; }
        public GetProductQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly FakeReadDatabase _database;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(FakeReadDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<ProductDto>(_database.GetData<Product>(w => (w as Product).Id == request.Id));
            return Task.FromResult(data);
        }
    }
}
