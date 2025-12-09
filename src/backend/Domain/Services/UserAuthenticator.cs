using Domain.Entities;
using Domain.Errors;
using Domain.Primitives;
using Domain.Repositories.Users;
using Domain.ValueObjects;

namespace Domain.Services
{
    //Passos complexos, mas que precisam ser centralizados, moram aqui
    //Isso poluiria a application, e é uma regra base, então vive aqui no domain
    //a app não precisa saber como é feito, ele só QUER q seja feito, ele orquestra.
    //tbm evita de ter mil valores de retorno diferente, já q agrega tudo aqui
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

            if (user is null || !user.PasswordHash.Verify(password))
            {
                return Result<User>.Failure(UserErrors.InvalidCredentials);
            }
            // result<generic>.metodo(oqvcquerdardps) (la ele)
            return Result<User>.Success(user);
        }
    }
}
