using MediatR;
using Domain.Primitives;
namespace Application.Commands.Auth.Login;

/* Utilizamos record em vez de class, pra deixar a response/request imutaveis
 * Por padrao, records vem com get e init, ou seja, o set só ocorre na inicialização
 * E também, tem o benefício de == funcionar pra valor, e não pro endereço de cada objeto
 * de loginCommand
 */
public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;