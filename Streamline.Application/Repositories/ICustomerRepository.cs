using Streamline.Domain.Entities.Customers;
using System.Threading.Tasks;

namespace Streamline.Application.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> PhoneExists(string phone);
        Task<bool> EmailExists(string email);
        Task<bool> DocumentExists(string document);
        void Add(Customer customer);
        Task<int> SaveChangesAsync();
    }
}
