using AutoMapper;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Models;
using Infra.Data.Queryables;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CidadeService : ICidadeService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public CidadeService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CidadeResponse>> Create(CreateCidadeRequest createCidadeRequest, CancellationToken cancellationToken = default)
    {
        var result = new Result<CidadeResponse>();

        if (string.IsNullOrEmpty(createCidadeRequest.Nome))
            result.WithError("O campo nome é obrigatório!");
        if (string.IsNullOrEmpty(createCidadeRequest.UF))
            result.WithError("O campo UF é obrigatório!");

        if (result.HasErro)
        {
            return result;
        }

        var cidade = _mapper.Map<Cidade>(createCidadeRequest);
        _repository.Add(cidade);
        await _repository.SaveChangesAsync(cancellationToken);

        result.Data = _mapper.Map<CidadeResponse>(cidade);
        return result;
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result();

        if (id == default)
        {
            result.WithError("O campo id é obrigatório!");
            return result;
        }

        var cidade = await _repository.GetAsync<Cidade>(id, cancellationToken);

        if (cidade != null)
        {
            _repository.Remove(cidade);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    public async Task<ResultPaginated<CidadeResponse>> Get(PaginatedRequest request, CancellationToken cancellationToken = default)
    {
        var result = new ResultPaginated<CidadeResponse>(request);
        var query = _repository.GetAll<Cidade>(@readonly: true);

        result.TotalCount = query.Count();

        var cidades = await query.Paginate(request.Page, request.CountPerPage).ToListAsync(cancellationToken);
        result.Data = _mapper.Map<IList<CidadeResponse>>(cidades);

        return result;
    }

    public async Task<Result<CidadeResponse>> Get(int id, CancellationToken cancellationToken = default)
    {
        var result = new Result<CidadeResponse>();
        var cidade = await _repository.GetAsync<Cidade>(id, cancellationToken);

        if (cidade != null)
        {
            result.Data = _mapper.Map<CidadeResponse>(cidade);
        }
        else
        {
            result.WithError("Não encontrado!");
        }

        return result;
    }

    public async Task<Result<CidadeResponse>> Update(UpdateCidadeRequest updateCidadeRequest, CancellationToken cancellationToken = default)
    {
        var result = new Result<CidadeResponse>();

        if (updateCidadeRequest.Id == default)
            result.WithError("O campo id é obrigatório!");
        if (string.IsNullOrEmpty(updateCidadeRequest.Nome))
            result.WithError("O campo nome é obrigatório!");
        if (string.IsNullOrEmpty(updateCidadeRequest.UF))
            result.WithError("O campo UF é obrigatório!");

        if (result.HasErro)
        {
            return result;
        }

        var cidade = await _repository.GetAsync<Cidade>(updateCidadeRequest.Id, cancellationToken);
        _mapper.Map(updateCidadeRequest, cidade);
        _repository.Update(cidade!);
        await _repository.SaveChangesAsync(cancellationToken);

        result.Data = _mapper.Map<CidadeResponse>(cidade);
        return result;
    }
}