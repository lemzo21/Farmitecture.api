using AutoMapper;
using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;

namespace Farmitecture.Api.Mapper;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        #region ProductMapper
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductDto>();
        #endregion

        #region Blogpost
         CreateMap<CreateBlogPostRequest, Blogpost>();
         CreateMap<Blogpost, BlogpostDto>();

         #endregion

         #region Cart

         CreateMap<Cart, CartDto>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
         CreateMap<CartItem, CartItemDto>()
             .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
         CreateMap<CreateCartItemRequest, CartItem>();
         #endregion
         
         #region Order
         CreateMap<Order, OrderDto>()
             .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
         CreateMap<OrderItem, OrderItemDto>();
         CreateMap<VerifiedData, OrderVerifiedData>();

         #endregion


    }
}