using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class LivroBiblicoConfiguracao : IEntityTypeConfiguration<LivroBiblico>
{
    public void Configure(EntityTypeBuilder<LivroBiblico> builder)
    {
        builder.Property(l => l.Testamento).IsRequired();
    }
}
