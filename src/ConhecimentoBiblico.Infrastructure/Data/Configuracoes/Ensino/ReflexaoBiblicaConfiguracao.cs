using ConhecimentoBiblico.Domain.Ensino;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class ReflexaoBiblicaConfiguracao : IEntityTypeConfiguration<ReflexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ReflexaoBiblica> builder)
    {
        builder.ToTable("ReflexoesBiblicas");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Explicacao).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.AplicacaoPratica).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.PerguntaReflexao).IsRequired().HasMaxLength(500);

        builder.HasOne<PerguntaBiblica>()
            .WithMany(p => p.Reflexoes)
            .HasForeignKey(r => r.PerguntaBiblicaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
