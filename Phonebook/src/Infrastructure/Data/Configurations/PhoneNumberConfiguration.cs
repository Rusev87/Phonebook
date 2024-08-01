using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Data.Configurations;
public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.HasKey(e => e.Id).HasName("phone_numbers_pkey");

        builder.ToTable("phone_numbers");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AddressId).HasColumnName("address_id");
        builder.Property(e => e.Number)
            .HasMaxLength(20)
            .HasColumnName("number");

        builder.HasOne(d => d.Address).WithMany(p => p.PhoneNumbers)
            .HasForeignKey(d => d.AddressId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("phone_numbers_address_id_fkey");
    }
}
