using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class LocalBiblico : ElementoBiblico
{
    private LocalBiblico() { }

    private LocalBiblico(Guid id, string nome, string descricao,
        string regiao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Local, elementoCentral)
    {
        Regiao = regiao;
    }

    public string Regiao { get; private set; } = string.Empty;

    public static LocalBiblico Criar(string nome, string descricao,
        string regiao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new LocalBiblico(Guid.NewGuid(), nome, descricao, regiao, elementoCentral);
    }
}
