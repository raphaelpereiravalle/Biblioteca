using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroValorController : ControllerBase
{
    private readonly ILivroValorService _service;
    private readonly ILogger<LivroValorController> _logger;

    public LivroValorController(ILivroValorService service, ILogger<LivroValorController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("listar-livros-valores")]
    [ProducesResponseType(typeof(PagedResult<LivroValorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListarLivrosValores([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar valores de livros paginados.");
            return StatusCode(500, new { message = "Erro interno ao listar valores de livros." });
        }
    }

    [HttpGet("obter-livro-valor{id:int}")]
    [ProducesResponseType(typeof(LivroValorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ObetarLivroValorPorId(int id)
    {
        try
        {
            var valor = await _service.GetByIdAsync(id);
            if (valor == null)
                return NotFound(new { message = $"LivroValor ID {id} não encontrado." });

            return Ok(valor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar LivroValor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao obter valor do livro." });
        }
    }

    [HttpPost("manter-livro-valor")]
    [ProducesResponseType(typeof(LivroValorDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ManterLivroValor([FromBody] LivroValorDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var valor = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(ObetarLivroValorPorId), new { id = valor.Data.IdLivroValor }, valor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar valor de livro.");
            return StatusCode(500, new { message = "Erro interno ao criar valor de livro." });
        }
    }

    [HttpPut("editar-livro-valor{id:int}")]
    [ProducesResponseType(typeof(LivroValorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> EditarLivroValor(int id, [FromBody] LivroValorDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado == null)
                return NotFound(new { message = $"LivroValor ID {id} não encontrado." });

            return Ok(atualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar LivroValor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao atualizar valor de livro." });
        }
    }

    [HttpDelete("deletar-livro-valor{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeletarLivroValor(int id)
    {
        try
        {
            var valor = await _service.GetByIdAsync(id);
            if (valor == null)
                return NotFound(new { message = $"LivroValor ID {id} não encontrado." });

            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir LivroValor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao excluir valor de livro." });
        }
    }
}
