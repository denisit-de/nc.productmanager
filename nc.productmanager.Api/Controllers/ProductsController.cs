using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nc.productmanager.Data;
using nc.productmanager.Data.Models;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nc.productmanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(
            AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Get all product categories")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all product categories", typeof(IEnumerable<ProductCategory>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categories not found")]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _db.ProductCategories.ToListAsync();
            return Ok(productCategories);
        }

        [HttpGet("categories/{id}")]
        [SwaggerOperation(Summary = "Get a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category found", typeof(ProductCategory))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            return Ok(productCategory);
        }

        [HttpPost("categories")]
        [SwaggerOperation(Summary = "Creates a product category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category created successfully", typeof(ProductCategory))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> CreateProductCategory(ProductCategory productCategory)
        {

            _db.Add(productCategory);
            await _db.SaveChangesAsync();

            return Ok(productCategory);
        }

        [HttpPut("categories/{id}")]
        [SwaggerOperation(Summary = "Update a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category updated successfully", typeof(ProductCategory))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> UpdateProductCategory(int id, ProductCategory category)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

            productCategory.Name = category.Name;

            _db.ProductCategories.Update(productCategory);
            await _db.SaveChangesAsync();

            return Ok(productCategory);
        }

        [HttpDelete("categories/{id}")]
        [SwaggerOperation(Summary = "Delete a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

            _db.ProductCategories.Remove(productCategory);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet()]
        [SwaggerOperation(Summary = "Get all products")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all products", typeof(IEnumerable<Product>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Products not found")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _db.Products
                                    .Include(p => p.ProductCategory)
                                    .ToListAsync();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product found", typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _db.Products
                                   .Include(p => p.ProductCategory)
                                   .FirstOrDefaultAsync(p => p.Id == id);

            return Ok(product);
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product created successfully", typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product category not found")]
        public async Task<IActionResult> CreateProduct(Product data)
        {

            _db.Add(data);
            await _db.SaveChangesAsync();

            return Ok(data);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated successfully", typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> UpdateProduct(int id, Product data)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            product.Name = data.Name;
            product.Price = data.Price;
            product.Description = data.Description;
            product.ProductCategoryId = data.ProductCategoryId;

            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok();
        }

    }
}

