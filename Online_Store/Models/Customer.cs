using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models
{
    public class Customer
    {
        [Required]
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
