using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands.Auth.Login;
using Domain.Errors;

namespace ApiExemplo.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        // 1. Manda pro Mediatr
        // o handler faz a logica e retorna Result<LoginResponse>
        var result = await _mediator.Send(command);

        // 2. Checa o result
        if (result.IsFailure)
        {
            // 3. Mapeia os erros de domínio em http (pois API cuida de http)
            return result.Error.Code switch
            {
                // usa referencia, evita string constante.
                UserErrors.InvalidCredentialsCode => Unauthorized(result.Error.Name),
                _ => BadRequest(result.Error.Name)
            };
        }

        return Ok(result.Value);
    }
}