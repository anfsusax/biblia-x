using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class TemaBiblico : ElementoBiblico
{
    private TemaBiblico() { }

    private TemaBiblico(Guid id, string nome, string descricao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Tema, elementoCentral) { }

    public static TemaBiblico Criar(string nome, string descricao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new TemaBiblico(Guid.NewGuid(), nome, descricao, elementoCentral);
    }
}
