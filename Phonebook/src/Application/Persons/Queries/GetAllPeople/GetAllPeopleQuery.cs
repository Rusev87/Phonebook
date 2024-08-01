using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Dtos;

namespace Phonebook.Application.Persons.Queries.GetAllPeople;
public record GetAllPeopleQuery : IRequest<List<PersonDto>>;

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, List<PersonDto>>
{
    private readonly IDapperRepository _repository;

    public GetAllPeopleQueryHandler(IDapperRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PersonDto>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllPeopleAsync();
    }
}
