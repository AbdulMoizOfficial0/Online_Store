using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Repositories;
using Online_Store.UnitOfWork;
namespace Online_Store.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Categories = new Repository<Category>(_context);
            Orders = new Repository<Order>(_context);
            Customers = new Repository<Customer>(_context);
            OrderItems = new Repository<OrderItem>(_context);
        }

        public IRepository<Product> Products { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Order> Orders { get; private set;}
        public IRepository<Customer> Customers { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }

        public IRepository<OrderItem> Items => throw new NotImplementedException();

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
