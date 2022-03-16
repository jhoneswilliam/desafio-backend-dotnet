using Infra.Data.Configurations;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    public class UnityOfWork : DbContext, IUnityOfWork
    {
        public UnityOfWork(DbContextOptions<UnityOfWork> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
