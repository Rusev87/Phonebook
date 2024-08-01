using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Dtos;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Queries.GetPersonById;
public class GetPersonByIdQuery : IRequest<PersonDto>
{
    public int Id { get; init; }
}

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    private readonly IMapper _mapper;
    private readonly IDapperRepository _repository;

    public GetPersonByIdQueryHandler(IDapperRepository repository , IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetPersonByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Person), $"{request.Id}");

        return entity;
    }
}
