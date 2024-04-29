using System.ComponentModel.DataAnnotations;

namespace nc.productmanager.Dto.Product
{
    public class ProductDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = $"Category must have a minimum of {{2}} and a maximum of {{1}} characters!")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = $"The product price must be greater than or equal to {{1}}.")]
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
    }

    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public GetProductCategoryDto ProductCategory { get; set; }
    }
}

