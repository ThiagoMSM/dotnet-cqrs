using Domain.Primitives;

namespace Domain.Errors;

public static class UserErrors
{
    // erros pro result pattern 
    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid email or password.");

    public static readonly Error NotFound = new(
        "User.NotFound",
        "User with the specified identifier was not found.");

    public static readonly Error AlreadyExists = new(
        "User.AlreadyExists",
        "User with this email already exists.");

}