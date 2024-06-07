using Online_Store.Models;
using Online_Store.Repositories;

namespace Online_Store.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<Category> Categories { get; }
        IRepository<OrderItem> Items { get; }
        IRepository<Customer> Customers { get; }
        Task<int> CompleteAsync();
    }
}
