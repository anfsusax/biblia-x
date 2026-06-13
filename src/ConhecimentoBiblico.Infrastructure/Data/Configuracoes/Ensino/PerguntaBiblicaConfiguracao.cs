using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class PerguntaBiblicaConfiguracao : IEntityTypeConfiguration<PerguntaBiblica>
{
    public void Configure(EntityTypeBuilder<PerguntaBiblica> builder)
    {
        builder.ToTable("PerguntasBiblicas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Pergunta)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(p => p.ElementosRelacionados)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PerguntaElementos",
                b => b.HasOne<ElementoBiblico>().WithMany()
                    .HasForeignKey("ElementoBiblicoId")
                    .OnDelete(DeleteBehavior.Cascade),
                b => b.HasOne<PerguntaBiblica>().WithMany()
                    .HasForeignKey("PerguntaBiblicaId")
                    .OnDelete(DeleteBehavior.Cascade),
                b => b.HasKey("PerguntaBiblicaId", "ElementoBiblicoId")
            );
    }
}
