using MediatR;
using Domain.Primitives;
namespace Application.Queries.User.GetUserByGuid;
public record GetUserByGuidQuery(Guid UserIdentifier) : IRequest<Result<GetUserByGuidResponse>>;
