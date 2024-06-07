namespace Online_Store.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
