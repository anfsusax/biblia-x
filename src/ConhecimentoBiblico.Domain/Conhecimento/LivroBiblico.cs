using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class LivroBiblico : ElementoBiblico
{
    private LivroBiblico() { }

    private LivroBiblico(Guid id, string nome, string descricao,
        Testamento testamento, int numeroCaps, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Livro, elementoCentral)
    {
        Testamento = testamento;
        NumeroCaps = numeroCaps;
    }

    public Testamento Testamento { get; private set; }
    public int NumeroCaps { get; private set; }

    public static LivroBiblico Criar(string nome, string descricao,
        Testamento testamento, int numeroCaps, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));
        if (numeroCaps <= 0)
            throw new ArgumentException("Número de capítulos deve ser maior que zero.", nameof(numeroCaps));

        return new LivroBiblico(Guid.NewGuid(), nome, descricao, testamento, numeroCaps, elementoCentral);
    }
}
