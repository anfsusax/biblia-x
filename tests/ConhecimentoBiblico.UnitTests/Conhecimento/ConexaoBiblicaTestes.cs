using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class ConexaoBiblicaTestes
{
    private static readonly Guid _origemId = Guid.NewGuid();
    private static readonly Guid _destinoId = Guid.NewGuid();

    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarConexao()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Jonas prefigura Cristo na ressurreição.");

        Assert.NotNull(conexao);
        Assert.Equal(_origemId, conexao.OrigemId);
        Assert.Equal(_destinoId, conexao.DestinoId);
        Assert.Equal(TipoConexao.PrefiguraCristo, conexao.TipoConexao);
        Assert.NotEqual(Guid.Empty, conexao.Id);
    }

    [Fact]
    public void Criar_ComOrigemIgualDestino_DeveLancarArgumentException()
    {
        var mesmoid = Guid.NewGuid();
        var ex = Assert.Throws<ArgumentException>(() =>
            ConexaoBiblica.Criar(mesmoid, mesmoid, TipoConexao.PrefiguraCristo, "Explicação."));

        Assert.Contains("Origem e Destino", ex.Message);
    }

    [Fact]
    public void Criar_ComExplicacaoVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            ConexaoBiblica.Criar(_origemId, _destinoId, TipoConexao.PrefiguraCristo, ""));

        Assert.Contains("Explicação", ex.Message);
    }

    [Fact]
    public void AdicionarFonte_ComReferenciaValida_DeveAdicionarFonte()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        conexao.AdicionarFonte("Mateus 12:40", "Pois assim como Jonas...");

        Assert.Single(conexao.FontesBiblicas);
        Assert.Equal("Mateus 12:40", conexao.FontesBiblicas.First().Referencia);
    }

    [Fact]
    public void AdicionarFonte_ComReferenciaVazia_DeveLancarArgumentException()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        Assert.Throws<ArgumentException>(() => conexao.AdicionarFonte(""));
    }

    [Fact]
    public void AdicionarFonte_SemTextoVersiculo_DevePermitirNulo()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        conexao.AdicionarFonte("João 5:46");

        Assert.Null(conexao.FontesBiblicas.First().TextoVersiculo);
    }
}
