using FluentMigrator;
using Domain.Entities;
namespace Infrastructure.Migrations
{
    [Migration(202512051522, "initial migration")] // YYYYMMDDmm, pouco legível, mas evita o versions.cs
    public class InitialMigration : VersionBase
    {
        public override void Up()
        {
            // Evita falta de sincronia entre migrations e domain. Utiliza-se nameof pra referenciar a entidade desejada na migration
            CreateInheritedTable(nameof(User))
                .WithColumn(nameof(User.UserIdentifier)).AsGuid().NotNullable().Unique()
                .WithColumn(nameof(User.FirstName)).AsString(50).NotNullable()
                .WithColumn(nameof(User.LastName)).AsString(50).NotNullable()
                .WithColumn(nameof(User.Email)).AsString(150).NotNullable().Unique()
                .WithColumn(nameof(User.PasswordHash)).AsString(255).NotNullable()
                .WithColumn(nameof(User.Cpf)).AsString(14).NotNullable();
        }
    }
}