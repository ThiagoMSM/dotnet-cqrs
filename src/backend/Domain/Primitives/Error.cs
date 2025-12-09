namespace Domain.Primitives;

public record Error(string Code, string Name)
{
    // None, "erro" para valores de sucesso
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
}