using Streamline.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Streamline.Infrastructure.Persistence.SqlServer.DbContexts;
using Streamline.Application.Repositories;
using System.Threading.Tasks;

namespace Streamline.Infrastructure.Persistence.SqlServer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlServerDbContext _context;

        public OrderRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Order.Add(order);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Opcional: carregar order com produtos e customer
        // public async Task<Order?> GetByIdAsync(int orderId)
        // {
        //     return await _context.Order
        //         .Include(o => o.Customer)
        //         .Include(o => o.OrderProduct)
        //             .ThenInclude(op => op.Product)
        //         .FirstOrDefaultAsync(o => o.Id == orderId);
        // }
    }
}
