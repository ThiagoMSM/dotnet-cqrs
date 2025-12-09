using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Domain.Primitives;
namespace Infrastructure.Migrations
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        // ???????????????/ QUEM USA CREATETABLE E CREATE.TABLE?? PELO AMOR DE DEUS
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateInheritedTable(string table)
        {
            return Create.Table(table)
                .WithColumn(nameof(BaseEntity.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(BaseEntity.CreatedAt)).AsDateTime().NotNullable()
                .WithColumn(nameof(BaseEntity.UpdatedAt)).AsDateTime().NotNullable()
                .WithColumn(nameof(BaseEntity.Active)).AsBoolean().NotNullable();
        }
    }
}   
