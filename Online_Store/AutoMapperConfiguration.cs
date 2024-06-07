using AutoMapper;
using Online_Store.DTOs;
using Online_Store.Models;

namespace Online_Store
{
    public class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Define mapping profiles
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();

                cfg.CreateMap<OrderItem, OrderItemDTO>();
                cfg.CreateMap<OrderItemDTO, OrderItem>();

                cfg.CreateMap<Order, OrderDTO>()
                   .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
                cfg.CreateMap<OrderDTO, Order>()
                   .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<CustomerDTO, Customer>();

                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}
