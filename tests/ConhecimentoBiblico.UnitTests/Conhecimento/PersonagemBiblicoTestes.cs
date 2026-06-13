using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class PersonagemBiblicoTestes
{
    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarPersonagem()
    {
        var personagem = PersonagemBiblico.Criar("Jesus", "Filho de Deus", "Século I d.C.", "Messias");

        Assert.NotNull(personagem);
        Assert.Equal("Jesus", personagem.Nome);
        Assert.Equal(TipoElementoBiblico.Personagem, personagem.TipoElemento);
        Assert.NotEqual(Guid.Empty, personagem.Id);
    }

    [Fact]
    public void Criar_ComElementoCentralTrue_DeveMarcarComoElementoCentral()
    {
        var personagem = PersonagemBiblico.Criar("Jesus", "Filho de Deus", "Século I d.C.", "Messias", elementoCentral: true);

        Assert.True(personagem.ElementoCentral);
    }

    [Fact]
    public void Criar_ComNomeVazio_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PersonagemBiblico.Criar("", "Descrição", "Século I d.C.", "Ocupação"));

        Assert.Contains("Nome", ex.Message);
    }

    [Fact]
    public void Criar_ComDescricaoVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PersonagemBiblico.Criar("Jesus", "", "Século I d.C.", "Messias"));

        Assert.Contains("Descrição", ex.Message);
    }

    [Fact]
    public void Criar_PorPadrao_NaoDeveSerElementoCentral()
    {
        var personagem = PersonagemBiblico.Criar("Jonas", "Profeta", "Século VIII a.C.", "Profeta");

        Assert.False(personagem.ElementoCentral);
    }
}
