namespace Application.Commands.Auth.Login;

//dto response do login
public record LoginResponse(string AccessToken, string FirstName);