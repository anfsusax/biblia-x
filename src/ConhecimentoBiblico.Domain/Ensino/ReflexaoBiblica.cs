namespace ConhecimentoBiblico.Domain.Ensino;

public sealed class ReflexaoBiblica
{
    private ReflexaoBiblica() { }

    private ReflexaoBiblica(Guid id, Guid perguntaBiblicaId, string titulo,
        string? explicacao, string? aplicacaoPratica, string? perguntaReflexao)
    {
        Id = id;
        PerguntaBiblicaId = perguntaBiblicaId;
        Titulo = titulo;
        Explicacao = explicacao;
        AplicacaoPratica = aplicacaoPratica;
        PerguntaReflexao = perguntaReflexao;
        CriadoEm = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid PerguntaBiblicaId { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string? Explicacao { get; private set; }
    public string? AplicacaoPratica { get; private set; }
    public string? PerguntaReflexao { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public static ReflexaoBiblica Criar(Guid perguntaBiblicaId, string titulo,
        string? explicacao = null, string? aplicacaoPratica = null, string? perguntaReflexao = null)
    {
        if (perguntaBiblicaId == Guid.Empty)
            throw new ArgumentException("PerguntaBiblicaId é obrigatório.", nameof(perguntaBiblicaId));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título é obrigatório.", nameof(titulo));

        return new ReflexaoBiblica(Guid.NewGuid(), perguntaBiblicaId, titulo,
            explicacao, aplicacaoPratica, perguntaReflexao);
    }
}
