using System.ComponentModel.DataAnnotations.Schema;

namespace nc.productmanager.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        [ForeignKey(nameof(ProductCategoryId))]
        public ProductCategory ProductCategory { get; set; }
    }
}

