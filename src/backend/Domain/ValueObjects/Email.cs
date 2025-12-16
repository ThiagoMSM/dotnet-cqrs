
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; } 
        private Email(string value) => Value = value; // Private set, só utilizado aqui dentro
        public static Email Create(string email) // Método de fábrica
        {

            //validações, etc
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(email))
                throw new ArgumentException($"Invalid email format: {email}", nameof(email));


            // ToLowerInvariant para evitar diferenças de alfabeto
            return new Email(email.Trim().ToLowerInvariant()); 
        }

        //helperszinhos:

        //faz o lookup ser bem tranquilo, string email sem casting
        public static implicit operator string(Email email) => email.Value;
        //faz casting de email ser possível passando por Create com o email desejado
        public static explicit operator Email(string email) => Create(email);

        // todo obj tem um ToString, e aqui o obj seria um Email { Value = test@example.com }, mas
        // a gente faz um override pra mostrar o Value diretamente, pra melhorar nos logs e etc
        public override string ToString() => Value;
    }
}
