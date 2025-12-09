
using System.Text.RegularExpressions;
// não se utiliza result patterns AQUI. result pattern é flow IN/OUT. isso é ENTRANHA
namespace Domain.ValueObjects
{
    //sealed, não é deriveable
    public sealed record Email
    {
        public string Value { get; } // não existe set, vc cria ele
        private Email(string value) => Value = value; //private set, só usado aqui dentro msm
        public static Email Create(string email) //método público
        {

            //validações, etc
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(email))
                throw new ArgumentException($"Invalid email format: {email}", nameof(email));
            
            
            //toLowerInvariant pra ser paranoico, pq o tolower é baseado em onde vc tá no mundo, e pode dar ruim
            return new Email(email.Trim().ToLowerInvariant()); // <-- chama o private cpf do construtor
        }

        //helperszinhos:

        //faz o lookup ser bem tranquilo, string a = myEmail: Email é possível sem casting
        public static implicit operator string(Email email) => email.Value;
        //faz casting de email ser possível passando por Create com o email desejado
        public static explicit operator Email(string email) => Create(email);

        // todo obj tem um ToString, e aqui o obj seria um Email { Value = test@example.com }, mas
        // a gente faz um override pra mostrar o Value diretamente, pra melhorar nos logs e etc
        public override string ToString() => Value;
    }
}
