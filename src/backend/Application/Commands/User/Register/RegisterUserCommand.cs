using MediatR;
using Domain.Primitives;
namespace Application.Commands.User.Register;
//dtos
public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password, string Cpf ) : IRequest<Result<RegisterUserResponse>>;
