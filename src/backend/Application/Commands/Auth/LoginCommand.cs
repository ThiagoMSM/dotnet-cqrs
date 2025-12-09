using MediatR;
using Domain.Shared;    
namespace Application.Commands.Auth;

//utilizamos record em vez de class, pra deixar a response/request imutaveis
//por padrao, records vem com get e init, ou seja, o set só ocorre na inicialização
//e tbm tem o beneficio de == funcionar pra valor, e não pro endereço de cada objeto
//de loginCommand

//dto é definido aqui
public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;