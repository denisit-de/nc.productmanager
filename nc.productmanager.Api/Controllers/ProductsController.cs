using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nc.productmanager.Data;
using nc.productmanager.Data.Models;
using nc.productmanager.Dto.Product;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nc.productmanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            AppDbContext db,
            IMapper mapper,
            ILogger<ProductsController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Get all product categories")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all product categories", typeof(IEnumerable<GetProductCategoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categories not found")]
        public async Task<IActionResult> GetProductCategories()
        {
            try
            {
                _logger.LogInformation("Fetching all product categories");
                var productCategories = await _db.ProductCategories.ToListAsync();
                var getProductCategoriesDto = _mapper.Map<IEnumerable<GetProductCategoryDto>>(productCategories);
                return Ok(getProductCategoriesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch product categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("categories/{id}")]
        [SwaggerOperation(Summary = "Get a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category found", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product category with ID: {Id}", id);
                var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (productCategory == null)
                {
                    _logger.LogWarning("Product category with ID: {Id} not found", id);
                    return NotFound();
                }

                var getProductCategoryDto = _mapper.Map<GetProductCategoryDto>(productCategory);
                return Ok(getProductCategoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch product category with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("categories")]
        [SwaggerOperation(Summary = "Creates a product category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category created successfully", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> CreateProductCategory(ProductCategoryDto categoryDto)
        {
            try
            {
                var productCategory = _mapper.Map<ProductCategory>(categoryDto);
                _db.Add(productCategory);
                await _db.SaveChangesAsync();

                var getProductCategoryDto = _mapper.Map<GetProductCategoryDto>(productCategory);
                _logger.LogInformation("Created a new product category with ID {CategoryId}", productCategory.Id);

                return Ok(getProductCategoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create product category");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("categories/{id}")]
        [SwaggerOperation(Summary = "Update a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category updated successfully", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> UpdateProductCategory(int id, ProductCategoryDto categoryDto)
        {
            try
            {
                var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (productCategory == null)
                {
                    _logger.LogWarning("Product category with ID: {Id} not found", id);
                    return NotFound();
                }

                _mapper.Map(categoryDto, productCategory);
                _db.ProductCategories.Update(productCategory);
                await _db.SaveChangesAsync();

                var getProductCategory = _mapper.Map<GetProductCategoryDto>(productCategory);
                _logger.LogInformation("Updated product category with ID {Id}", id);

                return Ok(getProductCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update product category with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("categories/{id}")]
        [SwaggerOperation(Summary = "Delete a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
                if (productCategory == null)
                {
                    _logger.LogWarning("Product category with ID: {Id} not found", id);
                    return NotFound();
                }

                _db.ProductCategories.Remove(productCategory);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Deleted product category with ID {Id}", id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete product category with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet()]
        [SwaggerOperation(Summary = "Get all products")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all products", typeof(IEnumerable<GetProductDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Products not found")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _logger.LogInformation("Fetching all products");
                var products = await _db.Products
                                        .Include(p => p.ProductCategory)
                                        .ToListAsync();
                var getProductsDto = _mapper.Map<IEnumerable<GetProductDto>>(products);
                return Ok(getProductsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch products");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product found", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product with ID: {Id}", id);
                var product = await _db.Products
                                       .Include(p => p.ProductCategory)
                                       .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {Id} not found", id);
                    return NotFound();
                }

                var getProductDto = _mapper.Map<GetProductDto>(product);
                return Ok(getProductDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch product with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product created successfully", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product category not found")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                _db.Add(product);
                await _db.SaveChangesAsync();

                var getProductDto = _mapper.Map<GetProductDto>(product);
                _logger.LogInformation("Created a new product with ID {ProductId}", product.Id);

                return Ok(getProductDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create product");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated successfully", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {Id} not found", id);
                    return NotFound();
                }

                _mapper.Map(productDto, product);
                _db.Products.Update(product);
                await _db.SaveChangesAsync();

                var getProductDto = _mapper.Map<GetProductDto>(product);
                _logger.LogInformation("Updated product with ID {Id}", id);

                return Ok(getProductDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update product with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {Id} not found", id);
                    return NotFound();
                }

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Deleted product with ID {Id}", id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete product with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}

