using Application.Commands.User.Register;
using Application.Queries.User.GetUserByGuid;
using Domain.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiExemplo.Controllers;

[Route("[controller]")]
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
                "User.AlreadyExists" => Conflict(result.Error.Name), // 409 Conflict
                _ => BadRequest(result.Error.Name)
            };
        }

        // 201 created diferente, é tipo um banquete pro frontend
        return CreatedAtAction(
            nameof(GetUserByGuid), // onde vc consegue pegar isso dnv (a rota get by id)
            new { id = result.Value.UserIdentifier }, // manda o id do user criado pra rota acima
            result.Value // volta o result pro body
        );
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByGuid([FromRoute] Guid id)
    {
        var query = new GetUserByGuidQuery(id);

        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return result.Error.Code switch
            {
                "User.NotFound" => NotFound(result.Error.Name),
                _ => BadRequest(result.Error.Name)
            };

        }
        return Ok(result.Value);
    }
}