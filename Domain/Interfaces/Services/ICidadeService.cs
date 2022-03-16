using Domain.DTO.Requests;
using Domain.DTO.Responses;

namespace Domain.Interfaces.Services;

public interface ICidadeService
{
    Task<ResultPaginated<CidadeResponse>> Get(PaginatedRequest request, CancellationToken cancellationToken = default);
    Task<ResultPaginated<PessoaResponse>> GetPessoas(int id, PaginatedRequest request, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Get(int id, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Create(CreateCidadeRequest createCidadeRequest, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Update(UpdateCidadeRequest updateCidadeRequest, CancellationToken cancellationToken = default);
    Task<Result> Delete(int id, CancellationToken cancellationToken = default);
}