using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PersonagemBiblicoConfiguracao : IEntityTypeConfiguration<PersonagemBiblico>
{
    public void Configure(EntityTypeBuilder<PersonagemBiblico> builder)
    {
        builder.Property(p => p.PeriodoHistorico).HasMaxLength(200);
        builder.Property(p => p.Ocupacao).HasMaxLength(200);

        builder.HasData(
            new { Id = DadosIniciais.IdJesus,  Nome = "Jesus",  Descricao = "Filho de Deus, o Messias prometido e cumprimento de todas as profecias.", TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = true,  PeriodoHistorico = "Século I d.C.",    Ocupacao = "Filho de Deus, Messias"  },
            new { Id = DadosIniciais.IdPaulo,  Nome = "Paulo",  Descricao = "Apóstolo dos gentios, responsável por explicar a teologia de Cristo.",     TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Teólogo"       },
            new { Id = DadosIniciais.IdPedro,  Nome = "Pedro",  Descricao = "Apóstolo e líder da Igreja primitiva.",                                     TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Pescador"      },
            new { Id = DadosIniciais.IdJoao,   Nome = "João",   Descricao = "Apóstolo e evangelista, discípulo amado de Jesus.",                         TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Evangelista"   },
            new { Id = DadosIniciais.IdJonas,  Nome = "Jonas",  Descricao = "Profeta cujos 3 dias no ventre do peixe prefiguram a ressurreição de Cristo.", TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século VIII a.C.", Ocupacao = "Profeta"                 },
            new { Id = DadosIniciais.IdMoises, Nome = "Moisés", Descricao = "Profeta e legislador, mediador da antiga aliança que prefigura Cristo.",      TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século XIII a.C.", Ocupacao = "Profeta, Legislador"     },
            new { Id = DadosIniciais.IdDavi,   Nome = "Davi",   Descricao = "Rei e salmista, cujo trono eterno aponta para o reinado de Cristo.",          TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século X a.C.",    Ocupacao = "Rei, Salmista"           }
        );
    }
}
