using Domain.Entities;
using Domain.Errors;
using Domain.Primitives;
using Domain.Repositories.Users;
using Domain.ValueObjects;
namespace Domain.Extensions;

public static class UserRepositoryExtensions
{
    /* Centraliza buscas com tratamento padronizado
     * Importante, evita de existir vários GetByIdOrFailureAsync e que cada um retorne erros diferentes
     * Centraliza passos simples e repetitivos, que extendem os métodos de infra (GetByIdAsync, GetByGuidAsync, etc.).
    */
    public static async Task<Result<User>> GetByIdOrFailureAsync(
        this IUserReadOnlyRepository repository, //extende o mesmo IUserReadOnlyRepository, pra facilitar com testes e na inheritance
        long id,
        CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);

        if (user is null)
        {
            return Result<User>.Failure(UserErrors.NotFound);
        }

        return Result<User>.Success(user);
    }
    public static async Task<Result<User>> GetByGuidOrFailureAsync(
        this IUserReadOnlyRepository repository, //extende o mesmo IUserReadOnlyRepository, pra facilitar com testes, na inheritance, etc
        Guid guid,
        CancellationToken ct = default)
    {
        var user = await repository.GetByGuidAsync(guid, ct);

        if (user is null)
        {
            return Result<User>.Failure(UserErrors.NotFound);
        }

        return Result<User>.Success(user);
    }
    public static async Task<Result<User>> GetByEmailOrFailureAsync(
        this IUserReadOnlyRepository repository,
        Email email,
        CancellationToken ct = default)
    {
        var user = await repository.GetByEmailAsync(email, ct);

        if (user is null)
        {
            return Result<User>.Failure(UserErrors.NotFound);
        }

        return Result<User>.Success(user);
    }
}