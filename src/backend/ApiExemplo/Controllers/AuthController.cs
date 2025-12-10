using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands.Auth.Login;

namespace API.Controllers;

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
            // 3. Mapeia os erros de domínio em http (pq controller cuida de http)
            return result.Error.Code switch
            {
                // usa a string constante (legal abstraír pra um canto único pra não dar 2 fontes de vdd)
                "User.InvalidCredentials" => Unauthorized(result.Error.Name),
                _ => BadRequest(result.Error.Name)
            };
        }

        // caso contrario, manda q funfou
        return Ok(result.Value);
    }
}