using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Ensino.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class PerguntaBiblicaConfiguracao : IEntityTypeConfiguration<PerguntaBiblica>
{
    public void Configure(EntityTypeBuilder<PerguntaBiblica> builder)
    {
        builder.ToTable("PerguntasBiblicas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Pergunta).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.CriadoEm).IsRequired();

        builder.HasMany(p => p.ElementosRelacionados)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PerguntaElementos",
                b => b.HasOne<ElementoBiblico>().WithMany()
                    .HasForeignKey("ElementoBiblicoId").OnDelete(DeleteBehavior.Cascade),
                b => b.HasOne<PerguntaBiblica>().WithMany()
                    .HasForeignKey("PerguntaBiblicaId").OnDelete(DeleteBehavior.Cascade),
                b =>
                {
                    b.HasKey("PerguntaBiblicaId", "ElementoBiblicoId");
                    b.HasData(
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaJonas,     ElementoBiblicoId = DadosIniciais.IdJonas         },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaJonas,     ElementoBiblicoId = DadosIniciais.IdJesus         },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaCasamento, ElementoBiblicoId = DadosIniciais.IdTemaCasamento },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaCasamento, ElementoBiblicoId = DadosIniciais.IdPaulo         }
                    );
                }
            );

        var dataSeed = new DateTime(2026, 6, 13, 0, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new { Id = DadosIniciais.IdPerguntaJonas,     Titulo = "Jonas e a Ressurreição", Pergunta = "Por que Jesus citou Jonas?",               Status = StatusPergunta.Pendente, CriadoEm = dataSeed },
            new { Id = DadosIniciais.IdPerguntaCasamento, Titulo = "O Que é Casamento?",     Pergunta = "O que a Bíblia ensina sobre casamento?",   Status = StatusPergunta.Pendente, CriadoEm = dataSeed }
        );
    }
}
