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
        builder.Property(r => r.Explicacao).HasMaxLength(2000);
        builder.Property(r => r.AplicacaoPratica).HasMaxLength(2000);
        builder.Property(r => r.PerguntaReflexao).HasMaxLength(500);
        builder.Property(r => r.CriadoEm).IsRequired();

        builder.HasOne<PerguntaBiblica>()
            .WithMany(p => p.Reflexoes)
            .HasForeignKey(r => r.PerguntaBiblicaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
