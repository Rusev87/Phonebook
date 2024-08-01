using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Phonebook.Application.Common.Interfaces;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
