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


    }
}