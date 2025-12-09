namespace Domain.ValueObjects
{
    //sealed pq vc não consegue implementar
    public sealed record Cpf // classe especial que facilita o lookup
                      // faz bater value com value (==) em vez de ===
    {
        public string Value { get; } // get publico pq user.cpf tem q ser visto, eu acho
        private Cpf(string value) => Value = value; // sei lá, construtor?
        //arrow expression shorthand pra não escrever {}...
 
        public static Cpf Create(string rawValue) //pq static?
            //static pois vc não precisa instanciar a classe cpf (cpf = new cpf) pra usar
        {
            //lógica de validação do VO
            if (string.IsNullOrWhiteSpace(rawValue))
                throw new Exception("CPF is required");
            var cleaned = rawValue.Replace(".", "").Replace("-", "");
            if (cleaned.Length != 11)
                throw new Exception("CPF must be 11 digits");
            //mais validações etc etc e tal
            return new Cpf(cleaned); // <-- chama o private cpf do construtor
        }
        //create chama lá, mas e o set? Set não existe, pq o create faz isso e o construtor já manda no value do get

        //é uma regra, não sei qual seria a diferença de escrever explicit em vez de implicit
        // mas td bem, é algo público, tipo um middleware? quando vc usa um cpf, na vdd, isso roda
        // antes, faz um hijacking do valor, e volta o cpf.Value quando vc usa o cpf
        // explicit precisa de casting ou tryparse, implicit assume q vc sabe oq tá fazendo
        // mesma ideia de vc usar '!' no fim de uma expressão q vc sabe q não vai dar ruim
        public static implicit operator string(Cpf cpf) => cpf.Value;

        public override string ToString() => Value;
    }
}
