namespace Phonebook.Application.Persons.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }

}
