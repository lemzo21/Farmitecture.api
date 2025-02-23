using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Farmitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<ProductDto>>))]
        public async Task<IActionResult> GetAllProducts([FromQuery] BaseFilter filter)
        {
            var products = await productRepository.GetAllProducts(filter);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ProductDto>))]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await productRepository.GetProductById(id);
            if (product.Data == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest product)
        {
            await productRepository.AddProduct(product);
            return Ok(default);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest product)
        {
            var existingProduct = await productRepository.GetProductById(id);
            if (existingProduct.Data == null)
            {
                return NotFound();
            }

            await productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await productRepository.GetProductById(id);
            if (product.Data == null)
            {
                return NotFound();
            }

            await productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}