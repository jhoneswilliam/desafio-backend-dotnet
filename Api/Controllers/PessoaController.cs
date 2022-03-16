using Domain.Interfaces.Services;
using Domain.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO.Requests;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PessoaResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result =  await _pessoaService.Get(id, cancellationToken);
        
        return !result.HasErro ? Ok(result) : NotFound(result);
    }

     [HttpGet("{id}/cidade")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<CidadeResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCidade(int id, CancellationToken cancellationToken)
    {
        var result =  await _pessoaService.GetCidade(id, cancellationToken);
        
        return !result.HasErro ? Ok(result) : NotFound(result);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultPaginated<PessoaResponse>))]
    public async Task<IActionResult> Get([FromQuery] PaginatedRequest paginatedRequest, CancellationToken cancellationToken)
    {
        return Ok(await _pessoaService.Get(paginatedRequest, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PessoaResponse>))]
    public async Task<IActionResult> Post([FromBody] CreatePessoaRequest createPessoaRequest, CancellationToken cancellationToken)
    {
        return Ok(await _pessoaService.Create(createPessoaRequest, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PessoaResponse>))]
    public async Task<IActionResult> Put([FromBody] UpdatePessoaRequest paginatedRequest, CancellationToken cancellationToken)
    {
        return Ok(await _pessoaService.Update(paginatedRequest, cancellationToken));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await _pessoaService.Delete(id, cancellationToken));
    }
}
