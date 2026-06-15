using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class EventoBiblicoConfiguracao : IEntityTypeConfiguration<EventoBiblico>
{
    public void Configure(EntityTypeBuilder<EventoBiblico> builder)
    {
        builder.HasData(
            new
            {
                Id = DadosIniciais.IdEventoBatismo,
                Nome = "Batismo de Jesus",
                Descricao = "Jesus é batizado por João Batista no Rio Jordão. O Espírito Santo desce sobre Ele como pomba e o Pai declara: 'Este é o meu Filho amado' (Mateus 3.13-17).",
                TipoElemento = TipoElementoBiblico.Evento,
                ElementoCentral = true
            },
            new
            {
                Id = DadosIniciais.IdEventoTentacaoDeserto,
                Nome = "Tentação no Deserto",
                Descricao = "Após o batismo, Jesus é levado pelo Espírito ao deserto onde permanece 40 dias e é tentado pelo diabo. Vence cada tentação com as Escrituras (Mateus 4.1-11).",
                TipoElemento = TipoElementoBiblico.Evento,
                ElementoCentral = true
            }
        );
    }
}
