using Domain.DTO.Requests;
using Domain.DTO.Responses;

namespace Domain.Interfaces.Services;

public interface ICidadeService
{
    Task<ResultPaginated<CidadeResponse>> Get(PaginatedRequest paginatedRequest, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Get(int id, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Create(CreateCidadeRequest createCidadeRequest, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> Update(UpdateCidadeRequest updateCidadeRequest, CancellationToken cancellationToken = default);
    Task<Result> Delete(int id, CancellationToken cancellationToken = default);
}