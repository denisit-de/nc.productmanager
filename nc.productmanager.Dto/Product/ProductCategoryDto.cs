using System.ComponentModel.DataAnnotations;

namespace nc.productmanager.Dto.Product
{
    public class ProductCategoryDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = $"Category must have a minimum of {{2}} and a maximum of {{1}} characters!")]
        public string Name { get; set; }
    }

    public class GetProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

