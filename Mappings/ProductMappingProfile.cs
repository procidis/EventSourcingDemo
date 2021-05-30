using AutoMapper;
using EventSourcingDemo.Commands;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Dtos;

namespace EventSourcingDemo.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<DeleteProductCommand, ProductDto>();
            CreateMap<UpdateProductCommand, ProductDto>();
        }
    }
}
