namespace Domain.ValueObjects;

using BCrypt.Net; 

public sealed record PasswordHash
{
    public string Value { get; }
    private PasswordHash(string value) => Value = value;

    // Criar password do 0
    public static PasswordHash CreateFromRaw(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Password cannot be empty");

        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        // Fluxo de criar password já engloba isso
        var hash = BCrypt.HashPassword(plainTextPassword);

        return new PasswordHash(hash);
    }

    // Carregar do db, para não re-hashear a senha
    public static PasswordHash LoadExisting(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be empty");

        return new PasswordHash(hash);
    }

    //verificação já no VO
    public bool IsValid(string plainTextPassword)
    {
        return BCrypt.Verify(plainTextPassword, Value);
    }

    // Consegue ser string sem casting
    public static implicit operator string(PasswordHash hash) => hash.Value;
    public override string ToString() => Value; // volta value em vez do obj
}