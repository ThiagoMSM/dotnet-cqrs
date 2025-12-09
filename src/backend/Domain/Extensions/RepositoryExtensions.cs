using Domain.Entities;
using Domain.Errors;
using Domain.Repositories.Users;
using Domain.Shared;
using Domain.ValueObjects;
namespace Domain.Extensions;

public static class UserRepositoryExtensions
{
    //centraliza buscas com tratamento padronizado
    //importantíssimo, evita de existir 30 GetByIdOrFailureAsync e que cada um retorne erros diferentes
    public static async Task<Result<User>> GetByIdOrFailureAsync(
        this IUserReadOnlyRepository repository, //extende o mesmo IUserReadOnlyRepository, pra facilitar com testes, na inheritance, etc
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
        this IUserReadOnlyRepository repository, //extende o mesmo IUserReadOnlyRepository, pra facilitar com testes, na inheritance, etc
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