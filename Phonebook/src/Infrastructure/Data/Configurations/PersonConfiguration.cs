
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data.Configurations;
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(e => e.Id).HasName("people_pkey");

        builder.ToTable("people");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.FullName)
            .HasMaxLength(100)
            .HasColumnName("full_name");
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .HasColumnName("email");
    }
}
