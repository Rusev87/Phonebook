using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Dtos;
public class UpdateAddressDto
{
    public int Id { get; set; }
    public AddressType Type { get; set; }
    public string AddressDetail { get; set; } = null!;
    public List<UpdatePhoneNumberDto> PhoneNumbers { get; set; } = new List<UpdatePhoneNumberDto>();
}
