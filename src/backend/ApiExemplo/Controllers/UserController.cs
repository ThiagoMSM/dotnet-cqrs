using Application.Commands.User.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiExemplo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost] // POST api/user
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return result.Error.Code switch
            {
                "User.AlreadyExists" => Conflict(result.Error), // 409 Conflict
                _ => BadRequest(result.Error)
            };
        }

        // 201 created diferente, é tipo um banquete pro frontend
        return CreatedAtAction(
            nameof(Register), // onde vc consegue pegar isso dnv (a rota get by id)
            new { id = result.Value.UserIdentifier }, // manda o id do user criado pra rota acima
            result.Value // volta o result pro body
        );
    }
}