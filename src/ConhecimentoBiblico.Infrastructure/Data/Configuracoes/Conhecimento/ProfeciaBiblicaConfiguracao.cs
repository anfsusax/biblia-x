using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ProfeciaBiblicaConfiguracao : IEntityTypeConfiguration<ProfeciaBiblica>
{
    public void Configure(EntityTypeBuilder<ProfeciaBiblica> builder)
    {
        builder.Property(p => p.TextoCumprimento).HasMaxLength(1000);
    }
}
