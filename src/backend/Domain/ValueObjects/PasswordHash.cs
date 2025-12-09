namespace Domain.ValueObjects;

using BCrypt.Net; 

public sealed record PasswordHash
{
    public string Value { get; }
    private PasswordHash(string value) => Value = value;

    // criar password do 0
    public static PasswordHash CreateFromRaw(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Password cannot be empty");

        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        // fluxo de criar password já engloba isso
        var hash = BCrypt.HashPassword(plainTextPassword);

        return new PasswordHash(hash);
    }

    // carregar do db, pq aparentemente o EF core faria merda sem isso
    public static PasswordHash LoadExisting(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be empty");

        return new PasswordHash(hash);
    }

    //verificação já no VO
    public bool Verify(string plainTextPassword)
    {
        return BCrypt.Verify(plainTextPassword, Value);
    }

    // consegue ser string sem casting
    public static implicit operator string(PasswordHash hash) => hash.Value;
    public override string ToString() => Value; // volta value em vez do obj
}