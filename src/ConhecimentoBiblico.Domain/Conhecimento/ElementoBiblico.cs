using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public abstract class ElementoBiblico
{
    protected ElementoBiblico() { }

    protected ElementoBiblico(Guid id, string nome, string descricao,
        TipoElementoBiblico tipoElemento, bool elementoCentral)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        TipoElemento = tipoElemento;
        ElementoCentral = elementoCentral;
    }

    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public TipoElementoBiblico TipoElemento { get; private set; }
    public bool ElementoCentral { get; private set; }
}
