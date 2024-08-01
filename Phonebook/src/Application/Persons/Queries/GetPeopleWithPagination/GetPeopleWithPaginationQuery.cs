using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Common.Models;
using Phonebook.Application.Persons.Dtos;

namespace Phonebook.Application.Persons.Queries.GetPeopleWithPagination;
public class GetPeopleWithPaginationQuery : IRequest<PaginatedList<PersonDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public class GetPeopleWithPaginationQueryHandler : IRequestHandler<GetPeopleWithPaginationQuery, PaginatedList<PersonDto>>
    {
        private readonly IDapperRepository _repository;

        public GetPeopleWithPaginationQueryHandler(IDapperRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<PersonDto>> Handle(GetPeopleWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _repository.GetPeopleCountAsync();
            var people = await _repository.GetPeopleWithPaginationAsync((request.PageNumber - 1) * request.PageSize, request.PageSize);

            return new PaginatedList<PersonDto>(people.ToList(), totalCount, request.PageNumber, request.PageSize);
        }
    }
}

