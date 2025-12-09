namespace Domain.ValueObjects
{
    //sealed pq vc não consegue implementar
    public sealed record Cpf // classe especial que facilita o lookup
                      // faz bater value com value (==) em vez de === (valor e nao endereço)
    {
        public string Value { get; } // get publico pq user.cpf tem q ser acessável
        private Cpf(string value) => Value = value;
        //arrow expression shorthand pra não escrever {}...

        //unica forma de interagir com o cpf é por esse método
        public static Cpf Create(string rawValue)
        {
            //lógica de validação do VO
            if (string.IsNullOrWhiteSpace(rawValue))
                throw new ArgumentException("CPF is required"); // <-- tecnicamente, deveria ser um erro padronizado
                                                                // não é result pattern pq é um erro de construção do objeto, não de fluxo
            var cleaned = rawValue.Replace(".", "").Replace("-", "");
            if (cleaned.Length != 11)
                throw new ArgumentException("CPF must be 11 digits");
            //mais validações etc etc e tal
            return new Cpf(cleaned); // <-- chama o private cpf do construtor
        }

        // faz o lookup ser mais fácil. sem casting do tipo Cpf necessário
        // para acessar o Value
        public static implicit operator string(Cpf cpf) => cpf.Value;

        // to string override para deixar os logs mais entendíveis (não vem com a estrutura do VO)
        public override string ToString() => Value;
    }
}
