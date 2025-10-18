using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _service;
    private readonly ILogger<AutorController> _logger;

    public AutorController(IAutorService service, ILogger<AutorController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/autor?pageNumber=1&pageSize=10
    [HttpGet("listar-autores")]
    [ProducesResponseType(typeof(PagedResult<AutorDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> listarAutores([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar autores paginados.");
            return StatusCode(500, new { message = "Erro interno ao listar autores." });
        }
    }

    [HttpGet("obter-autor/{id:int}")]
    [ProducesResponseType(typeof(AutorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var autor = await _service.GetByIdAsync(id);
            if (autor == null)
                return NotFound(new { message = $"Autor ID {id} não encontrado." });

            return Ok(autor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar autor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao obter autor." });
        }
    }

    [HttpPost("manter-autor")]
    [ProducesResponseType(typeof(AutorDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ManterAutor([FromBody] AutorDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var autor = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = autor.Data.IdAutor }, autor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar autor.");
            return StatusCode(500, new { message = "Erro interno ao criar autor." });
        }
    }

    [HttpPut("editar-autor{id:int}")]
    [ProducesResponseType(typeof(AutorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> EditarAutor(int id, [FromBody] AutorDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado == null)
                return NotFound(new { message = $"Autor ID {id} não encontrado." });

            return Ok(atualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar autor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao atualizar autor." });
        }
    }

    [HttpDelete("deletar-autor/{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeletarAutor(int id)
    {
        try
        {
            var autor = await _service.GetByIdAsync(id);
            if (autor == null)
                return NotFound(new { message = $"Autor ID {id} não encontrado." });

            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir autor ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao excluir autor." });
        }
    }
}
