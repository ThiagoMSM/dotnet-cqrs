namespace Application.Queries.User.GetUserByGuid;

public record GetUserByGuidResponse(
    Guid UserIdentifier, 
    string FirstName, 
    string LastName, 
    string Email);