namespace Phonebook.Application.Persons.Dtos;
public class UpdatePhoneNumberDto
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
}
