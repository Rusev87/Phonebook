namespace Phonebook.Application.Persons.Dtos
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string? Type { get; set; }
        public string? AddressDetail { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();
    }
}

