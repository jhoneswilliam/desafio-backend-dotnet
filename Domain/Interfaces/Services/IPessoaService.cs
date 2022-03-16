using Domain.DTO.Requests;
using Domain.DTO.Responses;

namespace Domain.Interfaces.Services;

public interface IPessoaService
{
    Task<ResultPaginated<PessoaResponse>> Get(PaginatedRequest paginated, CancellationToken cancellationToken = default);
    Task<Result<PessoaResponse>> Get(int id, CancellationToken cancellationToken = default);
    Task<Result<CidadeResponse>> GetCidade(int id, CancellationToken cancellationToken = default);
    Task<Result<PessoaResponse>> Create(CreatePessoaRequest id, CancellationToken cancellationToken = default);
    Task<Result<PessoaResponse>> Update(UpdatePessoaRequest id, CancellationToken cancellationToken = default);
    Task<Result> Delete(int id, CancellationToken cancellationToken = default);
}