using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Dtos;
public class CreateAddressDto
{
    public AddressType Type { get; set; } 
    public string AddressDetail { get; set; } = null!;
    public List<string> PhoneNumbers { get; set; } = null!;
}
