using Streamline.Domain.Entities;
using Streamline.Domain.Entities.Customers;
using Streamline.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Streamline.Infrastructure.Persistence.SqlServer.DbContexts
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {}

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Product> Product { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Base>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;  
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Address)
                .WithOne(c => c.Customer)
                .HasForeignKey<CustomerAddress>(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Contact)
                .WithOne(ct => ct.Customer)
                .HasForeignKey<CustomerContact>(ct => ct.CustomerId);
            
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(a => a.CustomerId);

            modelBuilder.Entity<CustomerContact>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<CustomerAddress>().ToTable("customer_address");
            modelBuilder.Entity<CustomerContact>().ToTable("customer_contact");
            modelBuilder.Entity<Product>().ToTable("product");
        }
    }
}
