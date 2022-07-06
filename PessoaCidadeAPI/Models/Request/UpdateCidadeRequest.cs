namespace PessoaCidadeAPI.Models.Request
{
    public class UpdateCidadeRequest
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }
    }
}
