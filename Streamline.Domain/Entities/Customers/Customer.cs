using BrazilianDocs;

namespace Streamline.Domain.Entities.Customers
{
    public class Customer : Base
    {
        public int Id { get; private set; } 
        public string Name { get; private set; }
        public string Document { get; private set; }
        public CustomerAddress? Address { get; private set; }
        public CustomerContact? Contact { get; private set; }

        protected Customer() { }

        public Customer(
            string name,
            string document,
            string phone,
            string email,
            string neighborhood,
            int number,
            string city,
            string state,
            string? complement = null)
        {
            Name = name; 
            SetDocument(document); 
            Contact = new CustomerContact(phone, email);
            Address = new CustomerAddress(neighborhood, number, city, state, complement);
        }

        private void SetDocument(string document)
        {
            if (!Cpf.IsValid(document))
                throw new InvalidOperationException("Document must be a valid CPF.");

            Document = document;
        }
    }
}
