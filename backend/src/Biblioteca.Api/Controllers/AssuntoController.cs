using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssuntoController : ControllerBase
{
    private readonly IAssuntoService _service;
    private readonly ILogger<AssuntoController> _logger;

    public AssuntoController(IAssuntoService service, ILogger<AssuntoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("listar-assuntos")]
    [ProducesResponseType(typeof(IEnumerable<AssuntoDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListarAssuntos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var assuntos = await _service.GetAllAsync();
            return Ok(assuntos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar assuntos.");
            return StatusCode(500, new { message = "Erro interno ao listar assuntos." });
        }
    }

    [HttpGet("obter-assunto{id:int}")]
    [ProducesResponseType(typeof(AssuntoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ObterAssuntoDtoPorId(int id)
    {
        try
        {
            var assunto = await _service.GetByIdAsync(id);
            if (assunto == null)
                return NotFound(new { message = $"Assunto ID {id} não encontrado." });

            return Ok(assunto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar assunto ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao obter assunto." });
        }
    }

    [HttpPost("manter-assunto")]
    [ProducesResponseType(typeof(AssuntoDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ManterAssunto([FromBody] AssuntoDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var assunto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(ObterAssuntoDtoPorId), new { id = assunto.Data.IdAssunto }, assunto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar assunto.");
            return StatusCode(500, new { message = "Erro interno ao criar assunto." });
        }
    }

    [HttpPut("editar-assunto/{id:int}")]
    [ProducesResponseType(typeof(AssuntoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> EditarAssunto(int id, [FromBody] AssuntoDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado == null)
                return NotFound(new { message = $"Assunto ID {id} não encontrado." });

            return Ok(atualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar assunto ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao atualizar assunto." });
        }
    }

    [HttpDelete("deletar-assunto/{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeletarAssunto(int id)
    {
        try
        {
            var assunto = await _service.GetByIdAsync(id);
            if (assunto == null)
                return NotFound(new { message = $"Assunto ID {id} não encontrado." });

            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir assunto ID {Id}", id);
            return StatusCode(500, new { message = "Erro interno ao excluir assunto." });
        }
    }
}
