using FluentMigrator;
using Domain.Entities;
namespace Infrastructure.Migrations
{
    [Migration(202512051522, "initial migration")] // YYYYMMDDmm, pouco legível, mas evita o versions.cs q é um CU
    public class InitialMigration : VersionBase
    {
        public override void Up()
        {
            //evita de errar os nomes, só existe UMA fonte de verdade para nomes, e ela está no domain
            //q é o centro do universo
            CreateInheritedTable(nameof(User))
                .WithColumn(nameof(User.UserIdentifier)).AsGuid().NotNullable().Unique()
                .WithColumn(nameof(User.FirstName)).AsString(50).NotNullable()
                .WithColumn(nameof(User.LastName)).AsString(50).NotNullable()
                .WithColumn(nameof(User.Email)).AsString(150).NotNullable().Unique() // Index included
                .WithColumn(nameof(User.PasswordHash)).AsString(255).NotNullable()
                .WithColumn(nameof(User.Cpf)).AsString(14).NotNullable();
        }
    }
}