using AutoMapper;
using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Repositories.Interfaces;
using Farmitecture.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Farmitecture.Api.Repositories.Providers
{

    public class ProductRepository(ApplicationDbContext context, IMapper mapper) : IProductRepository
    {
        public async Task<ApiResponse<PagedResult<IEnumerable<ProductDto>>>> GetAllProducts(BaseFilter filter)
        {
            try
            {
                var query =  context.Products.AsNoTracking();
                var res = await PaginationHelper.GetPaginatedResultAsync(
                    query,
                    filter.Page,
                    filter.PageSize,
                    mapper.Map<ProductDto>
                );
                return new ApiResponse<PagedResult<IEnumerable<ProductDto>>>()
                {
                    Code = StatusCodes.Status200OK,
                    Data = res,
                    IsSuccessful = true,
                    Message = "Products fetched successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedResult<IEnumerable<ProductDto>>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving products",
                    IsSuccessful = false
                };
            }
        }

        public async Task<ApiResponse<ProductDto>> GetProductById(Guid id)
        {
            try
            {
                var product = await context.Products.FindAsync(id);
                if (product == null)
                {
                    return new ApiResponse<ProductDto>()
                    {
                        Code = StatusCodes.Status404NotFound,
                        IsSuccessful = false,
                        Message = "Product not found"
                    };
                }

                return new ApiResponse<ProductDto>()
                {
                    Code = StatusCodes.Status200OK,
                    Data = mapper.Map<ProductDto>(product),
                    IsSuccessful = true,
                    Message = "Product fetched successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDto>()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task AddProduct(CreateProductRequest request)
        {
            var product = mapper.Map<Product>(request);
            product.CreatedAt = DateTime.UtcNow;
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(UpdateProductRequest request)
        {
            var product = mapper.Map<Product>(request);
                context.Entry(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid id)
        {
                var product = await context.Products.FindAsync(id);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
        }
    }
}