using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class ParabolaBiblica : ElementoBiblico
{
    private ParabolaBiblica() { }

    private ParabolaBiblica(Guid id, string nome, string descricao,
        string licaoCentral, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Parabola, elementoCentral)
    {
        LicaoCentral = licaoCentral;
    }

    public string LicaoCentral { get; private set; } = string.Empty;

    public static ParabolaBiblica Criar(string nome, string descricao,
        string licaoCentral, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new ParabolaBiblica(Guid.NewGuid(), nome, descricao, licaoCentral, elementoCentral);
    }
}
