using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class EnumsTestes
{
    [Fact]
    public void TipoElementoBiblico_DeveTerOitoTipos()
    {
        var valores = Enum.GetValues<TipoElementoBiblico>();
        Assert.Equal(8, valores.Length);
    }

    [Fact]
    public void TipoConexao_DeveConterPrefiguraCristo()
    {
        Assert.True(Enum.IsDefined(typeof(TipoConexao), TipoConexao.PrefiguraCristo));
    }

    [Fact]
    public void TipoConexao_DeveConterExplicadoPor_SemVariantesEspecificas()
    {
        Assert.True(Enum.IsDefined(typeof(TipoConexao), TipoConexao.ExplicadoPor));
        Assert.False(Enum.GetNames<TipoConexao>().Any(n => n.StartsWith("ExplicadoPor") && n != "ExplicadoPor"));
    }

    [Fact]
    public void Testamento_DeveConterAntigoENovo()
    {
        Assert.True(Enum.IsDefined(typeof(Testamento), Testamento.Antigo));
        Assert.True(Enum.IsDefined(typeof(Testamento), Testamento.Novo));
    }
}
