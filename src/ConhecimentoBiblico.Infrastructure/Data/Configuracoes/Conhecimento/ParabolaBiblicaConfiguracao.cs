using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ParabolaBiblicaConfiguracao : IEntityTypeConfiguration<ParabolaBiblica>
{
    public void Configure(EntityTypeBuilder<ParabolaBiblica> builder)
    {
        builder.Property(p => p.LicaoCentral).HasMaxLength(500);
    }
}
