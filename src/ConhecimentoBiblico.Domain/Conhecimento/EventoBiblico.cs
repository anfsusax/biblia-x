using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class EventoBiblico : ElementoBiblico
{
    private EventoBiblico() { }

    private EventoBiblico(Guid id, string nome, string descricao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Evento, elementoCentral) { }

    public static EventoBiblico Criar(string nome, string descricao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new EventoBiblico(Guid.NewGuid(), nome, descricao, elementoCentral);
    }
}
