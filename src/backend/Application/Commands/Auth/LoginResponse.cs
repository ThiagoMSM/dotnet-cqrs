namespace Application.Commands.Auth;

//dto response do login
public record LoginResponse(string AccessToken, string FirstName);