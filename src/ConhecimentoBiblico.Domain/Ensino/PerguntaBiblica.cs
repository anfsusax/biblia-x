using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Ensino.Enums;

namespace ConhecimentoBiblico.Domain.Ensino;

public sealed class PerguntaBiblica
{
    private readonly List<ElementoBiblico> _elementosRelacionados = new();
    private readonly List<ReflexaoBiblica> _reflexoes = new();

    private PerguntaBiblica() { }

    private PerguntaBiblica(Guid id, string titulo, string pergunta)
    {
        Id = id;
        Titulo = titulo;
        Pergunta = pergunta;
        Status = StatusPergunta.Pendente;
        CriadoEm = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Pergunta { get; private set; } = string.Empty;
    public StatusPergunta Status { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public IReadOnlyCollection<ElementoBiblico> ElementosRelacionados => _elementosRelacionados.AsReadOnly();
    public IReadOnlyCollection<ReflexaoBiblica> Reflexoes => _reflexoes.AsReadOnly();

    public static PerguntaBiblica Criar(string titulo, string pergunta)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(pergunta))
            throw new ArgumentException("Pergunta é obrigatória.", nameof(pergunta));

        return new PerguntaBiblica(Guid.NewGuid(), titulo, pergunta);
    }

    public void AvancarStatus()
    {
        Status = Status switch
        {
            StatusPergunta.Pendente    => StatusPergunta.EmEstudo,
            StatusPergunta.EmEstudo    => StatusPergunta.Respondida,
            StatusPergunta.Respondida  => StatusPergunta.Respondida,
            _ => Status
        };
    }

    public void AdicionarElementoRelacionado(ElementoBiblico elemento)
    {
        ArgumentNullException.ThrowIfNull(elemento);
        if (!_elementosRelacionados.Any(e => e.Id == elemento.Id))
            _elementosRelacionados.Add(elemento);
    }

    public void AdicionarReflexao(ReflexaoBiblica reflexao)
    {
        ArgumentNullException.ThrowIfNull(reflexao);
        _reflexoes.Add(reflexao);
    }
}
