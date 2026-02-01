using Streamline.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Streamline.Infrastructure.Persistence.SqlServer.DbContexts;
using Streamline.Application.Repositories;
using System.Threading.Tasks;

namespace Streamline.Infrastructure.Persistence.SqlServer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlServerDbContext _context;

        public CustomerRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PhoneExists(string phone)
        {
            return await _context.CustomerContacts.AnyAsync(c => c.Phone == phone);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.CustomerContacts.AnyAsync(c => c.Email == email);
        }

        public async Task<bool> DocumentExists(string document)
        {
            return await _context.Customer.AnyAsync(c => c.Document == document);
        }

        public void Add(Customer customer)
        {
            _context.Customer.Add(customer);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
