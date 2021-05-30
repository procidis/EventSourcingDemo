using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingDemo.Commands;
using EventSourcingDemo.Dtos;
using EventSourcingDemo.Extensions;
using EventSourcingDemo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace EventSourcingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDataResult<IEnumerable<ProductDto>>), 200)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return result.ToDataResult()
                .ToActionResult();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessDataResult<ProductDto>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));
            return result.ToDataResult()
                .ToActionResult();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task PostAsync([FromBody] CreateProductDto value)
        {
            await _mediator.Send(new CreateProductCommand { Product = value });
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] ProductDto value)
        {
            value.Id = id;
            await _mediator.Send(new UpdateProductCommand { Product = value });
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}
