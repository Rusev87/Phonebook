
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Common.Models;
using Phonebook.Application.Persons.Commands.CreatePerson;
using Phonebook.Application.Persons.Commands.DeletePerson;
using Phonebook.Application.Persons.Commands.UpdatePerson;
using Phonebook.Application.Persons.Dtos;
using Phonebook.Application.Persons.Queries.GetAllPeople;
using Phonebook.Application.Persons.Queries.GetPeopleWithPagination;
using Phonebook.Application.Persons.Queries.GetPersonById;

namespace Phonebook.Web.Endpoints;


public class Persons : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
                .MapPost(CreatePerson)
                .MapPut(UpdatePerson, "{id}")
                .MapDelete(DeletePerson, "{id}")
                .MapGet(GetPeopleWithPagination, "paginated") 
                .MapGet(GetPersonById, "{id}")
                .MapGet(GetAllPeople, "all");
    }

    public async Task<int> CreatePerson(ISender sender, CreatePersonCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdatePerson(ISender sender, int id, UpdatePersonCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeletePerson(ISender sender, int id)
    {
        await sender.Send(new DeletePersonCommand { Id = id });
        return Results.NoContent();
    }

    public async Task<PaginatedList<PersonDto>> GetPeopleWithPagination(
        ISender sender,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        var query = new GetPeopleWithPaginationQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return await sender.Send(query);
    }

    public async Task<PersonDto> GetPersonById(ISender sender, int id)
    {
        return await sender.Send(new GetPersonByIdQuery { Id = id });
    }

    public async Task<List<PersonDto>> GetAllPeople(ISender sender)
    {
        return await sender.Send(new GetAllPeopleQuery());
    }
}
