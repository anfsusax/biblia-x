using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Conhecimento;

namespace ConhecimentoBiblico.UnitTests.Ensino;

public class PerguntaBiblicaTestes
{
    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarPergunta()
    {
        var pergunta = PerguntaBiblica.Criar("Casamento Cristão", "O que é casamento?");

        Assert.NotNull(pergunta);
        Assert.Equal("O que é casamento?", pergunta.Pergunta);
        Assert.Equal("Casamento Cristão", pergunta.Titulo);
        Assert.NotEqual(Guid.Empty, pergunta.Id);
    }

    [Fact]
    public void Criar_ComPerguntaVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PerguntaBiblica.Criar("Título", ""));

        Assert.Contains("Pergunta", ex.Message);
    }

    [Fact]
    public void Criar_ComTituloVazio_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PerguntaBiblica.Criar("", "Pergunta válida?"));

        Assert.Contains("Título", ex.Message);
    }

    [Fact]
    public void Criar_DeveTerColecaoDeElementosVazia()
    {
        var pergunta = PerguntaBiblica.Criar("Título", "Pergunta?");

        Assert.Empty(pergunta.ElementosRelacionados);
    }

    [Fact]
    public void Criar_DeveTerColecaoDeReflexoesVazia()
    {
        var pergunta = PerguntaBiblica.Criar("Título", "Pergunta?");

        Assert.Empty(pergunta.Reflexoes);
    }
}
