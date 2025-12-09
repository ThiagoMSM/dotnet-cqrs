using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        // 1. checa se existe o information schema (se já existe o db)
        EnsureDatabaseCreated_MySql(connectionString);

        // roda a migration
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated_MySql(string connectionString)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        builder.Remove("Database");

        //using = quando acabar o método, mata essa var
        using var dbConnection = new MySqlConnection(builder.ConnectionString);

        //dapper aqui pra fazer essa query segura (com a morte do WP o dapper vai estar sendo
        // utilizado em incríveis 1 lugar. e esse lugar é a próxima linha de sql)
        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        // vê se o db existe
        var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

        if (!records.Any())
        {
            // não é possível parametrizar nome de db, então string interpolada
            dbConnection.Execute($"CREATE DATABASE `{databaseName}`");
        }
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        // scope para não vazar recursos
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        // Executa
        runner.MigrateUp();
    }
}