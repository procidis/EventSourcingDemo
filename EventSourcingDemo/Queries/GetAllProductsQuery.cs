using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Dtos;
using EventSourcingDemo.FakeServices;
using MediatR;

namespace EventSourcingDemo.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly FakeReadDatabase _database;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(FakeReadDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var data = _database.GetAllData<Product>().Select(w => _mapper.Map<ProductDto>(w));
            return Task.FromResult(data);
        }
    }
}
