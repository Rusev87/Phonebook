namespace Phonebook.Application.Persons.Dtos;
public class CreatePersonDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<CreateAddressDto> Addresses { get; set; } = null!;
}
