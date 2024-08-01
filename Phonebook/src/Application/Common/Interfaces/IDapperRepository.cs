using Phonebook.Application.Persons.Dtos;

namespace Phonebook.Application.Common.Interfaces;
public interface IDapperRepository
{
    Task<PersonDto> GetPersonByIdAsync(int id);
    Task<int> GetPeopleCountAsync();
    Task<IEnumerable<PersonDto>> GetPeopleWithPaginationAsync(int offset, int pageSize);
    Task<List<PersonDto>> GetAllPeopleAsync();
}
