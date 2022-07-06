namespace PessoaCidadeAPI.Models.Request
{
    public class UpdatePessoaRequest
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public int IdCidadeFK { get; set; }

        public int Idade { get; set; }
    }
}
