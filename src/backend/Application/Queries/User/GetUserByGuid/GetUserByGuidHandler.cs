using Domain.Extensions;
using Domain.Primitives;
using Domain.Repositories.Users;
using MediatR;

namespace Application.Queries.User.GetUserByGuid;

public class GetUserByGuidHandler : IRequestHandler<GetUserByGuidQuery, Result<GetUserByGuidResponse>>
{
    private readonly IUserReadOnlyRepository _readRepo;

    public GetUserByGuidHandler(
        IUserReadOnlyRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<Result<GetUserByGuidResponse>> Handle(GetUserByGuidQuery request, CancellationToken ct)
    {
        var userResult = await _readRepo.GetByGuidOrFailureAsync(request.UserIdentifier, ct);

        if (userResult.IsFailure) // ja vem erro da extensao acima 
        {   
            return Result<GetUserByGuidResponse>.Failure(userResult.Error);
        }

        var user = userResult.Value;

        var response = new GetUserByGuidResponse(
            user.UserIdentifier,
            user.FirstName,
            user.LastName,
            user.Email.Value
        );

        return Result<GetUserByGuidResponse>.Success(response);
    }
}
