namespace Streamline.Domain.Entities.Customers
{
    public class CustomerContact
    {
        public int CustomerId { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public Customer Customer { get; private set; }

        protected CustomerContact() { } 

        public CustomerContact(string phone, string email)
        {
            Phone = phone;
            Email = email; 
        }

    }
}
