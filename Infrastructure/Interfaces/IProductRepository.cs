using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int? id);
        Task<IReadOnlyList <Product>> GetProductsAsync();
        Task<IReadOnlyList <ProductBrand>> GetProductsBrandsAsync();
        Task<IReadOnlyList <ProductType>> GetProductsTypesAsync();

    }
}
