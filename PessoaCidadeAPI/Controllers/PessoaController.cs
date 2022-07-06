using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoaCidadeAPI.Models;
using PessoaCidadeAPI.Models.Request;
using PessoaCidadeAPI.Models.Response;

namespace PessoaCidadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase 
    {
        private readonly DataContext _context;

        public PessoaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PessoaResponse>>> Get()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            List<PessoaResponse> response = new List<PessoaResponse>();
            foreach (var p in pessoas)
            {
                response.Add(new PessoaResponse()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CPF = p.CPF,
                    Idade = p.Idade,
                    IdCidadeFK = p.IdCidadeFK                    
                });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPessoaResponse>> Get(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
                return BadRequest("Pessoa não encontrada");
            var cidade = await _context.Cidades.FindAsync(pessoa.IdCidadeFK);

            var response = new GetPessoaResponse()
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                CPF = pessoa.CPF,
                Idade = pessoa.Idade,
                IdCidadeFK = pessoa.IdCidadeFK,
                NomeCidade = cidade?.Nome,
                UF = cidade?.UF
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pessoa>>> Post(CreatePessoaRequest pessoa)
        {
            _context.Pessoas.Add(new Pessoa()
            {
                Nome = pessoa.Nome,
                CPF = pessoa.CPF,
                IdCidadeFK = pessoa.IdCidadeFK,
                Idade = pessoa.Idade
            });
            await _context.SaveChangesAsync();

            return Ok(await _context.Pessoas.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Pessoa>>> Put(UpdatePessoaRequest request)
        {
            var pessoa = await _context.Pessoas.FindAsync(request.Id);
            if (pessoa == null)
                return BadRequest("Pessoa não encontrada");

            pessoa.Nome = request.Nome;
            pessoa.CPF = request.CPF;
            pessoa.IdCidadeFK = request.IdCidadeFK;
            pessoa.Idade = request.Idade;

            await _context.SaveChangesAsync();

            return Ok(await _context.Pessoas.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pessoa>>> Delete(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
                return BadRequest("Pessoa não encontrada");

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pessoas.ToListAsync());
        }
    }
}
