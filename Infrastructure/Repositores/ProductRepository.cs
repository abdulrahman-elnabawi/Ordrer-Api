using Core.DbContexts;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync()
            => await _context.Set<ProductBrand>().ToListAsync();
        public async Task<Product> GetProductByIdAsync(int? id)
            => await _context.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Set<Product>().ToListAsync();




        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
            => await _context.Set<ProductType>().ToListAsync();
    }
}
