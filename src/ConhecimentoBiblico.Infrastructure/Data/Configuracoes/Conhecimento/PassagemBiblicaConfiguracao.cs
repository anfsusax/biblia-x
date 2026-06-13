using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PassagemBiblicaConfiguracao : IEntityTypeConfiguration<PassagemBiblica>
{
    public void Configure(EntityTypeBuilder<PassagemBiblica> builder)
    {
        builder.Property(p => p.Livro).HasMaxLength(100);
        builder.Property(p => p.TextoResumo).HasMaxLength(1000);
    }
}
