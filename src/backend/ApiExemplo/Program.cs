using Infrastructure;
using Infrastructure.Migrations;
using Application;
using API.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    DatabaseMigration.Migrate(connectionString ?? "", app.Services);

    Console.WriteLine("Database migration completed successfully.");
}
catch (Exception ex)
{

    Console.WriteLine($"CRITICAL ERROR: Migration failed. {ex.Message}");
    throw;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

/* Coisa do Dotnet/Roslyn. O program fica privado depois da compliação, aí falando
 * que ele é partial, é um workaround, pq aí ele permanece público para mocks e tests...
 */
public partial class Program { }