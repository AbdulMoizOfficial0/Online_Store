namespace Online_Store.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
