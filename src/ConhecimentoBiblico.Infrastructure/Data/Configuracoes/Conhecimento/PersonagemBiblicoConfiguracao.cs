using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PersonagemBiblicoConfiguracao : IEntityTypeConfiguration<PersonagemBiblico>
{
    public void Configure(EntityTypeBuilder<PersonagemBiblico> builder)
    {
        builder.Property(p => p.PeriodoHistorico).HasMaxLength(200);
        builder.Property(p => p.Ocupacao).HasMaxLength(200);
    }
}
