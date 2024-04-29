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

        public ProductsController(
            AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("categories")]
        [SwaggerOperation(Summary = "Get all product categories")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all product categories", typeof(IEnumerable<GetProductCategoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categories not found")]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _db.ProductCategories.ToListAsync();
            var getProductCategoriesDto = _mapper.Map<IEnumerable<GetProductCategoryDto>>(productCategories);
            return Ok(getProductCategoriesDto);
        }

        [HttpGet("categories/{id}")]
        [SwaggerOperation(Summary = "Get a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category found", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            var getProductCategoryDto = _mapper.Map<GetProductCategoryDto>(productCategory);
            return Ok(getProductCategoryDto);
        }

        [HttpPost("categories")]
        [SwaggerOperation(Summary = "Creates a product category")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category created successfully", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> CreateProductCategory(ProductCategoryDto categoryDto)
        {
            var productCategory = _mapper.Map<ProductCategory>(categoryDto);

            _db.Add(productCategory);
            await _db.SaveChangesAsync();

            var getProductCategoryDto = _mapper.Map<GetProductCategoryDto>(productCategory);
            return Ok(getProductCategoryDto);
        }

        [HttpPut("categories/{id}")]
        [SwaggerOperation(Summary = "Update a product category by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category updated successfully", typeof(GetProductCategoryDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category not found")]
        public async Task<IActionResult> UpdateProductCategory(int id, ProductCategoryDto categoryDto)
        {
            var productCategory = await _db.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

            _mapper.Map(categoryDto, productCategory);

            _db.ProductCategories.Update(productCategory);
            await _db.SaveChangesAsync();

            var getProductCategory = _mapper.Map<GetProductCategoryDto>(productCategory);
            return Ok(getProductCategory);
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
        [SwaggerResponse(StatusCodes.Status200OK, "List of all products", typeof(IEnumerable<GetProductDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Products not found")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _db.Products
                                    .Include(p => p.ProductCategory)
                                    .ToListAsync();

            var getProductsDto = _mapper.Map<IEnumerable<GetProductDto>>(products);
            return Ok(getProductsDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product found", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _db.Products
                                   .Include(p => p.ProductCategory)
                                   .FirstOrDefaultAsync(p => p.Id == id);

            var getProductDto = _mapper.Map<GetProductDto>(product);
            return Ok(getProductDto);
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product created successfully", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product category not found")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _db.Add(product);
            await _db.SaveChangesAsync();

            var getProductDto = _mapper.Map<GetProductDto>(product);
            return Ok(getProductDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a product by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Product updated successfully", typeof(GetProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            _mapper.Map(productDto, product);

            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            var getProductDto = _mapper.Map<GetProductDto>(product);
            return Ok(getProductDto);
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

