using Phonebook.Domain.Entities;

namespace Phonebook.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Person> People { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
