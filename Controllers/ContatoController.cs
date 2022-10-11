using Dio.AgendaDeContatos.Context;
using Dio.AgendaDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dio.AgendaDeContatos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        private readonly ILogger<ContatoController> _logger;

        public ContatoController(AgendaContext context, ILogger<ContatoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            try
            {
                throw new Exception();

                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = contato.Id }, contato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Title = "Erro",
                    Msg = $"Erro ao criar registro!",
                    Status = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Contato> listaDeContatos = _context.Contatos.ToList();

            if (listaDeContatos == null)
                return NotFound();

            return Ok(listaDeContatos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Contato contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return Ok(contato);
        }

        [HttpGet("ObterPorNome/{nome}")]
        public IActionResult BuscaPorNome([FromRoute] string nome)
        {
            var listaDeContatos = _context.Contatos.Where(c => c.Nome.Contains(nome));

            if (listaDeContatos == null)
                return NoContent();

            return Ok(listaDeContatos);
        }

        [HttpPut("atualizar/{id:int}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] Contato contato)
        {
            Contato contatoToUpdate = _context.Contatos.Find(id);

            if (contatoToUpdate == null)
                return NotFound();

            contatoToUpdate.Nome = contato.Nome;
            contatoToUpdate.Telefone = contato.Telefone;
            contatoToUpdate.Ativo = contato.Ativo;

            _context.SaveChanges();

            return Ok(contatoToUpdate);
        }

        [HttpDelete("deletar/{id:int}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            Contato contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            _context.Remove(contato);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
