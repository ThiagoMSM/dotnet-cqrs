namespace Domain.ValueObjects
{
    //sealed para mitigar implementação de herança desnecessária
    public sealed record Cpf // classe especial que facilita o lookup
                             // faz bater value com value (==) em vez de === (valor e nao endereço)
    {
        public string Value { get; } // get publico pq user.cpf tem q ser acessável
        private Cpf(string value) => Value = value;

        // Unica forma de interagir com o cpf se dá por esse método
        public static Cpf Create(string rawValue)
        {
            // Lógica de validação do VO
            if (string.IsNullOrWhiteSpace(rawValue))
                throw new ArgumentException("CPF is required"); // <-- tecnicamente, deveria ser um erro padronizado
                                                                // não é result pattern pois é um erro de construção do objeto, não de fluxo
            var cleaned = rawValue.Replace(".", "").Replace("-", "");
            if (cleaned.Length != 11)
                throw new ArgumentException("CPF must be 11 digits");
            // ...Mais validações

            return new Cpf(cleaned); // <-- chama o private cpf do construtor
        }

        // Faz o lookup ser mais fácil. Sem casting do tipo Cpf necessário para acessar o Value
        public static implicit operator string(Cpf cpf) => cpf.Value;

        // ToString override para deixar os logs mais entendíveis (não vem com a estrutura do VO)
        public override string ToString() => Value;
    }
}
