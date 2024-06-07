using System.ComponentModel.DataAnnotations;

namespace Online_Store.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public CategoryDTO Category { get; set; }
    }
}
