using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class TemaBiblicoConfiguracao : IEntityTypeConfiguration<TemaBiblico>
{
    public void Configure(EntityTypeBuilder<TemaBiblico> builder)
    {
        builder.HasData(
            new { Id = DadosIniciais.IdTemaFe,        Nome = "Fé",        Descricao = "Confiança e crença em Deus e em Seus propósitos.",                    TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaCasamento, Nome = "Casamento", Descricao = "União entre homem e mulher que reflete a relação de Cristo e a Igreja.", TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaPerdao,    Nome = "Perdão",    Descricao = "A graça de Deus que perdoa os pecados através de Cristo.",               TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaEsperanca, Nome = "Esperança", Descricao = "A certeza das promessas de Deus cumpridas em Cristo.",                   TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaSalvacao,  Nome = "Salvação",  Descricao = "A redenção do ser humano através do sacrifício de Jesus Cristo.",        TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false }
        );
    }
}
