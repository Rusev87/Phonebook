using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Dtos;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Commands.CreatePerson
{
    public record CreatePersonCommand : IRequest<int>
    {
        public string FullName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public List<CreateAddressDto> Addresses { get; init; } = null!;
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                FullName = request.FullName,
                Email = request.Email
            };

            if (request.Addresses != null && request.Addresses.Any())
            {
                foreach (var addressDto in request.Addresses)
                {
                    var address = new Address
                    {
                        Type = addressDto.Type,
                        AddressDetail = addressDto.AddressDetail,
                        PhoneNumbers = addressDto.PhoneNumbers.Select(pn => new PhoneNumber { Number = pn }).ToList()
                    };

                    person.Addresses.Add(address);
                }
            }

            _context.People.Add(person);
            await _context.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
