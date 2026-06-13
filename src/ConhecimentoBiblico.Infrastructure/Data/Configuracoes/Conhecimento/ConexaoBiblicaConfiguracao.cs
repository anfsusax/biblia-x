using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ConexaoBiblicaConfiguracao : IEntityTypeConfiguration<ConexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ConexaoBiblica> builder)
    {
        builder.ToTable("ConexoesBiblicas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Explicacao).IsRequired().HasMaxLength(2000);
        builder.Property(c => c.TipoConexao).IsRequired();

        builder.HasOne(c => c.Origem).WithMany()
            .HasForeignKey(c => c.OrigemId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Destino).WithMany()
            .HasForeignKey(c => c.DestinoId).OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(c => c.FontesBiblicas, fb =>
        {
            fb.ToTable("ConexaoFontesBiblicas");
            fb.Property<int>("Id").ValueGeneratedOnAdd();
            fb.HasKey("Id");
            fb.WithOwner().HasForeignKey("ConexaoBiblicaId");
            fb.Property(f => f.Referencia).IsRequired().HasMaxLength(50);
            fb.Property(f => f.TextoVersiculo).HasMaxLength(500);

            fb.HasData(
                new { Id = 1, ConexaoBiblicaId = DadosIniciais.IdConexaoJonasJesus,          Referencia = "Mateus 12:40",    TextoVersiculo = (string?)null },
                new { Id = 2, ConexaoBiblicaId = DadosIniciais.IdConexaoMoisesJesus,          Referencia = "João 5:46",       TextoVersiculo = (string?)null },
                new { Id = 3, ConexaoBiblicaId = DadosIniciais.IdConexaoDaviJesus,            Referencia = "Lucas 20:41-44",  TextoVersiculo = (string?)null },
                new { Id = 4, ConexaoBiblicaId = DadosIniciais.IdConexaoJonasCitadoPorJesus,  Referencia = "Mateus 12:39",    TextoVersiculo = (string?)null },
                new { Id = 5, ConexaoBiblicaId = DadosIniciais.IdConexaoCasamentoPaulo,       Referencia = "Efésios 5:25-32", TextoVersiculo = (string?)null },
                new { Id = 6, ConexaoBiblicaId = DadosIniciais.IdConexaoPerdaoPaulo,          Referencia = "Efésios 4:32",    TextoVersiculo = (string?)null }
            );
        });

        builder.HasData(
            new { Id = DadosIniciais.IdConexaoJonasJesus,         OrigemId = DadosIniciais.IdJonas,         DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.PrefiguraCristo,  Explicacao = "Jonas ficou 3 dias no ventre do peixe, prefigurando os 3 dias de Jesus no sepulcro e Sua ressurreição." },
            new { Id = DadosIniciais.IdConexaoMoisesJesus,         OrigemId = DadosIniciais.IdMoises,        DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.PrefiguraCristo,  Explicacao = "Moisés como mediador da antiga aliança prefigura Cristo, mediador da nova e eterna aliança." },
            new { Id = DadosIniciais.IdConexaoDaviJesus,           OrigemId = DadosIniciais.IdDavi,          DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.ApontaParaCristo, Explicacao = "O trono eterno prometido a Davi aponta para o reino eterno de Jesus Cristo." },
            new { Id = DadosIniciais.IdConexaoJonasCitadoPorJesus, OrigemId = DadosIniciais.IdJonas,         DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.CitadoPorJesus,   Explicacao = "Jesus citou Jonas explicitamente ao responder sobre o sinal do Filho do Homem (Mt 12:39-40)." },
            new { Id = DadosIniciais.IdConexaoCasamentoPaulo,      OrigemId = DadosIniciais.IdTemaCasamento, DestinoId = DadosIniciais.IdPaulo,  TipoConexao = TipoConexao.ExplicadoPor,     Explicacao = "Paulo ensina que o casamento é reflexo da relação de Cristo com a Igreja em Efésios 5:25-32." },
            new { Id = DadosIniciais.IdConexaoPerdaoPaulo,         OrigemId = DadosIniciais.IdTemaPerdao,    DestinoId = DadosIniciais.IdPaulo,  TipoConexao = TipoConexao.ExplicadoPor,     Explicacao = "Paulo explica o perdão como graça de Deus em Cristo, exortando os crentes a perdoarem uns aos outros." }
        );
    }
}
