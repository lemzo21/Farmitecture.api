namespace Farmitecture.Api.Repositories.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    Product GetProductById(int id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
}