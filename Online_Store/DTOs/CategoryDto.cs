namespace Online_Store.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ProductDTO> Products { get; set; }
    }
}
