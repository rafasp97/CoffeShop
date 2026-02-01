using Streamline.Application.Repositories;
using Streamline.Domain.Entities.Customers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Streamline.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await ValidateCreation(request.Phone, request.Email, request.Document);

            var customer = new Customer(
                request.Name,
                request.Document,
                request.Phone,
                request.Email,
                request.Neighborhood,
                request.Number,
                request.City,
                request.State,
                request.Complement
            );

            _customerRepository.Add(customer);
            await _customerRepository.SaveChangesAsync();

            return new CreateCustomerResult
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Contact.Email
            };


        }

        private async Task ValidateCreation(string phone, string email, string document)
        {
            if (await _customerRepository.EmailExists(email))
                throw new InvalidOperationException("Email is already registered.");

            if (await _customerRepository.PhoneExists(phone))
                throw new InvalidOperationException("Phone number is already registered.");

            if (await _customerRepository.DocumentExists(document))
                throw new InvalidOperationException("Document is already registered.");
        }
    }
}
