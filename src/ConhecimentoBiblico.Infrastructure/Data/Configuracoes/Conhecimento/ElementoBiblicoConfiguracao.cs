using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ElementoBiblicoConfiguracao : IEntityTypeConfiguration<ElementoBiblico>
{
    public void Configure(EntityTypeBuilder<ElementoBiblico> builder)
    {
        builder.ToTable("ElementosBiblicos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Descricao)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(e => e.ElementoCentral)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasDiscriminator<TipoElementoBiblico>(e => e.TipoElemento)
            .HasValue<PersonagemBiblico>(TipoElementoBiblico.Personagem)
            .HasValue<TemaBiblico>(TipoElementoBiblico.Tema)
            .HasValue<EventoBiblico>(TipoElementoBiblico.Evento)
            .HasValue<PassagemBiblica>(TipoElementoBiblico.Passagem)
            .HasValue<ProfeciaBiblica>(TipoElementoBiblico.Profecia)
            .HasValue<ParabolaBiblica>(TipoElementoBiblico.Parabola)
            .HasValue<LivroBiblico>(TipoElementoBiblico.Livro)
            .HasValue<LocalBiblico>(TipoElementoBiblico.Local);
    }
}
