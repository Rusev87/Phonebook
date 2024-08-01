using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Dtos;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Commands.UpdatePerson;
public record UpdatePersonCommand : IRequest<int>
{
    public int Id { get; init; }
    public string FullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public List<UpdateAddressDto> Addresses { get; init; } = new List<UpdateAddressDto>();
}

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdatePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.PhoneNumbers)
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (entity == null) throw new NotFoundException(nameof(Person), $"{request.Id}");

        entity.FullName = request.FullName;
        entity.Email = request.Email;

        foreach (var addressDto in request.Addresses)
        {
            var addressEntity = entity.Addresses.FirstOrDefault(a => a.Id == addressDto.Id);
            if (addressEntity != null)
            {
                addressEntity.Type = addressDto.Type;
                addressEntity.AddressDetail = addressDto.AddressDetail;

                foreach (var phoneNumberDto in addressDto.PhoneNumbers)
                {
                    var phoneNumberEntity = addressEntity.PhoneNumbers.FirstOrDefault(p => p.Id == phoneNumberDto.Id);
                    if (phoneNumberEntity != null)
                    {
                        phoneNumberEntity.Number = phoneNumberDto.Number;
                    }
                    else
                    {
                        addressEntity.PhoneNumbers.Add(new PhoneNumber
                        {
                            Number = phoneNumberDto.Number,
                            AddressId = addressEntity.Id
                        });
                    }
                }
            }
            else
            {
                var newAddress = new Address
                {
                    Type = addressDto.Type,
                    AddressDetail = addressDto.AddressDetail,
                    PersonId = entity.Id
                };

                foreach (var phoneNumberDto in addressDto.PhoneNumbers)
                {
                    newAddress.PhoneNumbers.Add(new PhoneNumber
                    {
                        Number = phoneNumberDto.Number
                    });
                }

                entity.Addresses.Add(newAddress);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
