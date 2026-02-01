using Streamline.Domain.Entities;

namespace Streamline.Domain.Entities.Products
{
    public class Product : Base
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }
        public int StockQuantity { get; private set; }
        public bool Active { get; private set; }

        protected Product() { }

        public Product(
            string name,
            decimal price,
            int stockQuantity,
            bool active,
            string? description = null)
        {
            Name = name;
            SetPrice(price);
            SetStockQuantity(stockQuantity);
            Active = active;
            Description = description;
        }

        private void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new InvalidOperationException("Price must be greater than zero.");

            Price = price;
        }

        private void SetStockQuantity(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative.");

            StockQuantity = quantity;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative.");

            StockQuantity = quantity;
        }

        public void Activate() => Active = true;

        public void Deactivate() {
            Active = false;
            //TODO: adicionar lógica para retirar de todos pedidos
        }
    }
}
