using Domain.Primitives;

namespace Domain.Errors;

public static class UserErrors
{
    public const string InvalidCredentialsCode = "User.InvalidCredentials";
    public const string NotFoundCode = "User.NotFound";
    public const string AlreadyExistsCode = "User.AlreadyExists";

    public static readonly Error InvalidCredentials = new(
        InvalidCredentialsCode,
        "Invalid email or password.");

    public static readonly Error NotFound = new(
        NotFoundCode,
        "User with the specified identifier was not found.");

    public static readonly Error AlreadyExists = new(
        AlreadyExistsCode,
        "User with this email already exists.");
}