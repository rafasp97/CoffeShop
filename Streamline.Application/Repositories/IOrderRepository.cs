using Streamline.Domain.Entities.Orders;
using System.Threading.Tasks;

namespace Streamline.Application.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Task<int> SaveChangesAsync();
    }
}
