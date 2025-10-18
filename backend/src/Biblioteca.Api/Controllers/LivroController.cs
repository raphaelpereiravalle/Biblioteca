using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _service;
    private readonly ILogger<LivroController> _logger;

    public LivroController(ILivroService service, ILogger<LivroController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/livro
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<LivroDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var livros = await _service.GetAllAsync();
            return Ok(livros);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar livros.");
            return StatusCode(500, new { message = "Erro interno ao listar livros." });
        }
    }

    // GET: api/livro/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LivroDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var livro = await _service.GetByIdAsync(id);
            if (livro == null)
                return NotFound(new { message = $"Livro ID {id} não encontrado." });

            return Ok(livro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar livro ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao obter livro." });
        }
    }

    // POST: api/livro
    [HttpPost]
    [ProducesResponseType(typeof(LivroDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Post([FromBody] LivroDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var livro = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = livro.IdLivro }, livro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar livro.");
            return StatusCode(500, new { message = "Erro interno ao criar livro." });
        }
    }

    // PUT: api/livro/{id}
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(LivroDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Put(int id, [FromBody] LivroDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado == null)
                return NotFound(new { message = $"Livro ID {id} não encontrado." });

            return Ok(atualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar livro ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao atualizar livro." });
        }
    }

    // DELETE: api/livro/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var livro = await _service.GetByIdAsync(id);
            if (livro == null)
                return NotFound(new { message = $"Livro ID {id} não encontrado." });

            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir livro ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao excluir livro." });
        }
    }
}
