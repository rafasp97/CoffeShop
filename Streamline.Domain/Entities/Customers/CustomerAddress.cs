namespace Streamline.Domain.Entities.Customers
{
    public class CustomerAddress
    {
        public int CustomerId { get; private set; }
        public string Neighborhood { get; private set; }
        public int Number { get; private set; }
        public string? Complement { get; set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Customer Customer { get; private set; }

        protected CustomerAddress() { }

        public CustomerAddress(string neighborhood, int number, string city, string state, string? complement = null)
        {
            Neighborhood = neighborhood; 
            Number = number;             
            City = city;                
            State = state.ToUpper();    
            Complement = complement;
        }

    }
}
