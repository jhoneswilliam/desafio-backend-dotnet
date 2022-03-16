using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using AutoMapper;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Models;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Infra.Data;
using Infrastructure;

namespace Infra.CrossCutting;

public class NativeInjector
{
    public static void Setup(IServiceCollection services, IConfiguration configuration)
    {
        RegisterRepositories(services, configuration);
        RegisterServices(services, configuration);
    }

    private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IUnityOfWork, UnityOfWork>(options =>
        {            
            var connection = Environment.GetEnvironmentVariable("DBConnection") ?? configuration.GetConnectionString("Default"); 
            options.UseSqlServer(connection, b =>
            {
                b.MigrationsAssembly("Api");
            })
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

        services.AddScoped<IRepository, Repository>();
    }

    private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new MapperConfiguration(mc =>
        {
            mc.CreateMap<CreateCidadeRequest, Cidade>();
            mc.CreateMap<UpdateCidadeRequest, Cidade>();
            mc.CreateMap<Cidade, CidadeResponse>();

            mc.CreateMap<CreatePessoaRequest, Pessoa>();
            mc.CreateMap<UpdatePessoaRequest, Pessoa>();
            mc.CreateMap<Pessoa, PessoaResponse>();
        }).CreateMapper());

        services.AddScoped<ICidadeService, CidadeService>();
        services.AddScoped<IPessoaService, PessoaService>();
    }
}