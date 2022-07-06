namespace PessoaCidadeAPI.Models.Response
{
    public class GetCidadeResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }

        public List<Habitante> Habitantes { get; set; }
    }
}
