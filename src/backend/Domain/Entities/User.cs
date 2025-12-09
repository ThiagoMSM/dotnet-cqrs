using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserIdentifier { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Cpf Cpf { get; private set; }

        // construtor privado (nenhuma classe consegue bypass os métodos do user)
        private User(Guid userIdentifier, string firstName, string lastName, Email email, PasswordHash passwordHash, Cpf cpf)
        {
            UserIdentifier = userIdentifier;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Cpf = cpf;

        }
        // create publico (as classes PRECISAM usar um dos metodos)
        public static User Create(string firstName, string lastName, Email email, PasswordHash passwordHash, Cpf cpf)
        {
            return new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                passwordHash,
                cpf
                );
        }
    }
}
