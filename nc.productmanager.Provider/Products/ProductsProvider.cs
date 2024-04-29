using System;
using Microsoft.EntityFrameworkCore;
using nc.productmanager.Data;
using nc.productmanager.Data.Models;
using nc.productmanager.Provider.Interfaces;

namespace nc.productmanager.Provider.Products
{
	public class ProductsProvider : IProductsProvider
	{
        private readonly AppDbContext _db;

        public ProductsProvider(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _db.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            return await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory category)
        {
            _db.ProductCategories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(ProductCategory category)
        {
            _db.ProductCategories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(ProductCategory category)
        {
            _db.ProductCategories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.Products.Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
                                 .Include(p => p.ProductCategory)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}

