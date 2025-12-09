using Application.Interfaces;
using Domain.Services;
using Domain.Shared;
using MediatR;

namespace Application.Commands.Auth;
// este handler, ORQUESTRA. não faz nada mais além da ORQUESTRAÇÃO, top to bottom.
public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserAuthenticator _userAuth;
    public LoginHandler(ITokenGenerator tokenGenerator, UserAuthenticator userAuth)
    {
        _tokenGenerator = tokenGenerator;
        _userAuth = userAuth;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand req, CancellationToken ct)
    {
        // usa um método externo, mas esse método externo é burro, aqui q ele fica inteligente
        var authResult = await _userAuth.AuthenticateAsync(req.Email, req.Password, ct);
        // pq o retorno dele é tratado
        if (authResult.IsFailure) // ...é testado
        {
            return Result<LoginResponse>.Failure(authResult.Error); // ...é devolvido
        }
        // ...é extraído
        var user = authResult.Value;
        // define a geração de token
        var token = _tokenGenerator.Generate(user);

        var response = new LoginResponse(token, user.FirstName);
        // e volta a resposta
        return Result<LoginResponse>.Success(response);
    }
}