using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;

namespace Farmitecture.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ApiResponse<PagedResult<IEnumerable<ProductDto>>>> GetAllProducts(BaseFilter filter);
    Task<ApiResponse<ProductDto>> GetProductById(Guid id);
    Task AddProduct(CreateProductRequest request);
    Task UpdateProduct(UpdateProductRequest request);
    Task DeleteProduct(Guid id);
}