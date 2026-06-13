using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ConexaoBiblicaConfiguracao : IEntityTypeConfiguration<ConexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ConexaoBiblica> builder)
    {
        builder.ToTable("ConexoesBiblicas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Explicacao)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.TipoConexao).IsRequired();

        builder.HasOne(c => c.Origem)
            .WithMany()
            .HasForeignKey(c => c.OrigemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Destino)
            .WithMany()
            .HasForeignKey(c => c.DestinoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(c => c.FontesBiblicas, fb =>
        {
            fb.ToTable("ConexaoFontesBiblicas");
            fb.Property<int>("Id").ValueGeneratedOnAdd();
            fb.HasKey("Id");
            fb.WithOwner().HasForeignKey("ConexaoBiblicaId");
            fb.Property(f => f.Referencia).IsRequired().HasMaxLength(50);
            fb.Property(f => f.TextoVersiculo).HasMaxLength(500);
        });
    }
}
