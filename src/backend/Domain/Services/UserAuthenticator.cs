using Domain.Entities;
using Domain.Errors;
using Domain.Primitives;
using Domain.Repositories.Users;
using Domain.ValueObjects;

namespace Domain.Services
{
    /* Em prol da centralização do Domain, e da simplificação da Application,
     * esses serviços agem como helpers para a Application.
     * Centralizam passos procedurais comuns, que envolvem múltiplos repositórios ou múltiplas etapas.
     * Não se encaixam em Extensions, pois não "extendem" nenhum método de infra, mas sim, criam rotinas tangentes.
     * E também, são mais complexos que os passos centralizados em Extensions
     * */
    public class UserAuthenticator
    {
        private readonly IUserReadOnlyRepository _repo;

        public UserAuthenticator(IUserReadOnlyRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<User>> AuthenticateAsync(string email, string password, CancellationToken ct)
        {
            var user = await _repo.GetByEmailAsync(Email.Create(email), ct);

            if (user is null || !user.PasswordHash.IsValid(password))
            {
                return Result<User>.Failure(UserErrors.InvalidCredentials);
            }
            return Result<User>.Success(user);
        }
    }
}
