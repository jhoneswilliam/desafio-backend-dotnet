using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable(nameof(Pessoa));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        builder.Property(x => x.Nome).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CPF).HasMaxLength(11).IsRequired();
        builder.Property(x => x.Idade).IsRequired();

        builder.HasOne(x => x.Cidade)
            .WithMany(x => x.Pessoas);
    }
}