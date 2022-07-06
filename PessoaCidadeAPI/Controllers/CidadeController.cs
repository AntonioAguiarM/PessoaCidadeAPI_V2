using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoaCidadeAPI.Models;
using PessoaCidadeAPI.Models.Request;
using PessoaCidadeAPI.Models.Response;

namespace PessoaCidadeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly DataContext _context;

        public CidadeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CidadeResponse>>> Get()
        {
            var cidades = await _context.Cidades.ToListAsync();
            List<CidadeResponse> response = new List<CidadeResponse>();
            foreach (var c in cidades)
            {
                response.Add(new CidadeResponse()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    UF = c.UF
                });
            }
            return Ok(response);
            return Ok(await _context.Cidades.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCidadeResponse>> Get(int id)
        {
            var cidade = await _context.Cidades.FindAsync(id);
            var pessoas = await _context.Pessoas.ToListAsync();
            var habitantes = pessoas.Where(p => id.Equals(p.IdCidadeFK)).ToList();

            if (cidade == null)
                return BadRequest("Cidade não encontrada");

            List<Habitante> habitantesResponse = new List<Habitante>();
            foreach (var h in habitantes) 
            {
                habitantesResponse.Add(new Habitante()
                {
                    Nome = h.Nome,
                    CPF = h.CPF,
                    Idade = h.Idade
                });
            }

            var response = new GetCidadeResponse()
            {
                Id = cidade.Id,
                Nome = cidade.Nome,
                UF = cidade.UF,
                Habitantes = habitantesResponse
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<List<Cidade>>> Post(CreateCidadeRequest cidade)
        {
            _context.Cidades.Add(new Cidade()
            {
                Nome = cidade.Nome,
                UF = cidade.UF
            });
            await _context.SaveChangesAsync();

            return Ok(await _context.Cidades.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Cidade>>> Put(UpdateCidadeRequest request)
        {
            var cidade = await _context.Cidades.FindAsync(request.Id);
            if (cidade == null)
                return BadRequest("Cidade não encontrada");
            
            cidade.Nome = request.Nome;
            cidade.UF = request.UF;

            await _context.SaveChangesAsync();

            return Ok(await _context.Cidades.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cidade>>> Delete(int id)
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
                return BadRequest("Cidade não encontrada");

            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cidades.ToListAsync());
        }
    }
}
