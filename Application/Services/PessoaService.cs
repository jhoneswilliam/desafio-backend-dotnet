using AutoMapper;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Models;
using Infra.Data.Queryables;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public PessoaService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PessoaResponse>> Create(CreatePessoaRequest createPessoaRequest, CancellationToken cancellationToken = default)
    {
        var result = new Result<PessoaResponse>();

        if (string.IsNullOrEmpty(createPessoaRequest.Nome))
            result.WithError("O campo Nome é obrigatório!");
        if (string.IsNullOrEmpty(createPessoaRequest.CPF))
            result.WithError("O campo CPF é obrigatório!");
        if (createPessoaRequest.Idade == default)
            result.WithError("O campo Idade é obrigatório!");
        if (createPessoaRequest.IdCidade == default)
            result.WithError("O campo Cidade é obrigatório!");

        if (result.HasErro)
            return result;

        if (createPessoaRequest.CPF!.Length != 11)
            result.WithError("O campo CPF é invalido!");

        if (createPessoaRequest.Idade <= 0 || createPessoaRequest.Idade > 120)
            result.WithError("O campo Idade é invalido!");

        if (result.HasErro)
            return result;

        var pessoa = _mapper.Map<Pessoa>(createPessoaRequest);
        pessoa.Cidade = await _repository.GetAsync<Cidade>(createPessoaRequest.IdCidade);

        if (pessoa.Cidade == null)
        {
            result.WithError("O campo Cidade é invalido!");
            return result;
        }

        _repository.Add(pessoa);
        await _repository.SaveChangesAsync(cancellationToken);

        result.Data = _mapper.Map<PessoaResponse>(pessoa);
        return result;
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result();

        if (id == default)
        {
            result.WithError("O campo Id é obrigatório!");
            return result;
        }

        var pessoa = await _repository.GetAsync<Pessoa>(id, cancellationToken);

        if (pessoa != null)
        {
            _repository.Remove(pessoa);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public async Task<ResultPaginated<PessoaResponse>> Get(PaginatedRequest request, CancellationToken cancellationToken = default)
    {
        var result = new ResultPaginated<PessoaResponse>(request);
        var query = _repository.GetAll<Pessoa>(@readonly: true);

        result.TotalCount = query.Count();

        var pessoas = await query.Paginate(request.Page, request.CountPerPage).ToListAsync(cancellationToken);
        result.Data = _mapper.Map<IList<PessoaResponse>>(pessoas);

        return result;
    }

    public async Task<Result<PessoaResponse>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result<PessoaResponse>();
        var pessoa = await _repository.GetAsync<Pessoa>(id, cancellationToken);

        if (pessoa != null)
        {
            result.Data = _mapper.Map<PessoaResponse>(pessoa);
        }
        else
        {
            result.WithError("Não encontrado!");
        }

        return result;
    }

    public async Task<Result<CidadeResponse>> GetCidade(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result<CidadeResponse>();
        var pessoa = await _repository
            .GetAll<Pessoa>(@readonly: true)
            .Include(x => x.Cidade)
            .FirstAsync(x => x.Id == id);

        if (pessoa != null)
        {
            result.Data = _mapper.Map<CidadeResponse>(pessoa.Cidade);
        }
        else
        {
            result.WithError("Não encontrado!");
        }

        return result;
    }

    public async Task<Result<PessoaResponse>> Update(UpdatePessoaRequest updatePessoaRequest, CancellationToken cancellationToken = default)
    {
        var result = new Result<PessoaResponse>();

        if (updatePessoaRequest.Id == default)
            result.WithError("O campo id é obrigatório!");
        if (string.IsNullOrEmpty(updatePessoaRequest.Nome))
            result.WithError("O campo Nome é obrigatório!");
        if (string.IsNullOrEmpty(updatePessoaRequest.CPF))
            result.WithError("O campo CPF é obrigatório!");
        if (updatePessoaRequest.Idade == default)
            result.WithError("O campo Idade é obrigatório!");
        if (updatePessoaRequest.IdCidade == default)
            result.WithError("O campo Cidade é obrigatório!");

        if (result.HasErro)
            return result;

        if (updatePessoaRequest.CPF!.Length != 11)
            result.WithError("O campo CPF é invalido!");

        if (updatePessoaRequest.Idade <= 0 || updatePessoaRequest.Idade > 120)
            result.WithError("O campo Idade é invalido!");

        if (result.HasErro)
            return result;

        var pessoa = await _repository.GetAsync<Pessoa>(updatePessoaRequest.Id, cancellationToken);

        if (pessoa == null)
        {
            result.WithError("O campo Id é obrigatório!");
            return result;
        }

        _mapper.Map(updatePessoaRequest, pessoa);

        pessoa.Cidade = await _repository.GetAsync<Cidade>(updatePessoaRequest.IdCidade);
        if (pessoa.Cidade == null)
        {
            result.WithError("O campo Cidade é invalido!");
            return result;
        }

        _repository.Update(pessoa!);
        await _repository.SaveChangesAsync(cancellationToken);

        result.Data = _mapper.Map<PessoaResponse>(pessoa);
        return result;
    }
}