using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class LocalBiblicoConfiguracao : IEntityTypeConfiguration<LocalBiblico>
{
    public void Configure(EntityTypeBuilder<LocalBiblico> builder)
    {
        builder.Property(l => l.Regiao).HasMaxLength(200);
    }
}
