namespace PessoaCidadeAPI.Models.Request
{
    public class CreatePessoaRequest
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public int IdCidadeFK { get; set; }

        public int Idade { get; set; }
    }
}
