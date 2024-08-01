
using Phonebook.Application.Common.Interfaces;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.Persons.Commands.DeletePerson;
public record DeletePersonCommand : IRequest<int>
{
    public int Id { get; init; }
}

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeletePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.PhoneNumbers)
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (entity == null) throw new NotFoundException(nameof(Person), $"{request.Id}");

        _context.People.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

