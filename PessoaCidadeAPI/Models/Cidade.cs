namespace PessoaCidadeAPI.Models
{
    public class Cidade
    {
        public Cidade()
        {
            Habitantes = new List<Pessoa>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }

        public List<Pessoa> Habitantes { get; set; }

    }
}
