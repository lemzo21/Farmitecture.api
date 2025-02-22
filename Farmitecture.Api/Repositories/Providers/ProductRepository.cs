using Farmitecture.Api.Data.Context;
using Farmitecture.Api.Repositories.Interfaces;

namespace Farmitecture.Api.Repositories.Providers
{

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Set<Product>().ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Set<Product>().Find(id);
        }

        public async Task AddProduct(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task DeleteProduct(int id)
        {
            var product = _context.Set<Product>().Find(id);
            if (product != null)
            {
                _context.Set<Product>().Remove(product);
                _context.SaveChanges();
            }
        }
    }
}