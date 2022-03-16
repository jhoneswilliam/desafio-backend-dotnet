using Domain.Interfaces.Services;
using Domain.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO.Requests;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CidadeController : ControllerBase
{
    private readonly ICidadeService _cidadeService;

    public CidadeController(ICidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<CidadeResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result =  await _cidadeService.Get(id, cancellationToken);
        
        return !result.HasErro ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id}/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultPaginated<PessoaResponse>))]
    public async Task<IActionResult> GetPessoas(int id, [FromQuery] PaginatedRequest paginatedRequest, CancellationToken cancellationToken)
    {
        return Ok(await _cidadeService.GetPessoas(id, paginatedRequest, cancellationToken));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultPaginated<CidadeResponse>))]
    public async Task<IActionResult> Get([FromQuery] PaginatedRequest paginatedRequest, CancellationToken cancellationToken)
    {
        return Ok(await _cidadeService.Get(paginatedRequest, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<CidadeResponse>))]
    public async Task<IActionResult> Post([FromBody] CreateCidadeRequest createCidadeRequest, CancellationToken cancellationToken)
    {
        return Ok(await _cidadeService.Create(createCidadeRequest, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<CidadeResponse>))]
    public async Task<IActionResult> Put([FromBody] UpdateCidadeRequest paginatedRequest, CancellationToken cancellationToken)
    {
        return Ok(await _cidadeService.Update(paginatedRequest, cancellationToken));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await _cidadeService.Delete(id, cancellationToken));
    }
}
