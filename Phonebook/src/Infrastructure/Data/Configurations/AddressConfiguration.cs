using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data.Configurations;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(e => e.Id).HasName("addresses_pkey");

        builder.ToTable("addresses");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AddressDetail)
            .HasMaxLength(200)
            .HasColumnName("address_detail");
        builder.Property(e => e.PersonId).HasColumnName("person_id");
        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("type");

        builder.HasOne(d => d.Person).WithMany(p => p.Addresses)
            .HasForeignKey(d => d.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("addresses_person_id_fkey");

        builder.Property(e => e.Type)
            .HasConversion(
                v => v.ToString(),
                v => (AddressType)Enum.Parse(typeof(AddressType), v));
    }
}
