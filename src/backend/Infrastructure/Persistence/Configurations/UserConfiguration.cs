using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Configurations;

// é publico por convenção, e pra facilitar com essa inheritance
// mas em termos DDD puristas, seria internal
public class UserConfiguration : IEntityTypeConfiguration<User> //passa user como generic
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(u => u.Cpf)
            .HasMaxLength(14)
            .IsRequired()
            .HasConversion(
                cpf => cpf.Value,
                value => Cpf.Create(value)
            );


        builder.Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired()
            .HasConversion(
                email => email.Value,             // Write: Email -> String
                value => Email.Create(value)      // Read: String -> Email
            );

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(255)
            .IsRequired()
            .HasConversion(
                pass => pass.Value,                       // Write
                value => PasswordHash.LoadExisting(value) // Read (Prevent double-hashing!)
            );
    }
}