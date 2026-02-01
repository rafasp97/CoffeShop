using Streamline.Domain.Entities;
using Streamline.Domain.Entities.Customers;
using Streamline.Domain.Entities.Products;
using Streamline.Domain.Entities.Orders;
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
        public DbSet<Order> Order { get; set; }

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

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Customer)  
                .WithMany()                 
                .HasForeignKey(c => c.CustomerId)
                .IsRequired()                 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProduct)  
                .WithOne(op => op.Order)         
                .HasForeignKey(op => op.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); 
            
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()                    
                .HasForeignKey(op => op.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<CustomerAddress>().ToTable("customer_address");
            modelBuilder.Entity<CustomerContact>().ToTable("customer_contact");
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<Order>().ToTable("order");
            modelBuilder.Entity<OrderProduct>().ToTable("order_product");
        }
    }
}
