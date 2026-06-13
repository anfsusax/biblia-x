using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class ProfeciaBiblica : ElementoBiblico
{
    private ProfeciaBiblica() { }

    private ProfeciaBiblica(Guid id, string nome, string descricao,
        bool cumprida, string? textoCumprimento, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Profecia, elementoCentral)
    {
        Cumprida = cumprida;
        TextoCumprimento = textoCumprimento;
    }

    public bool Cumprida { get; private set; }
    public string? TextoCumprimento { get; private set; }

    public static ProfeciaBiblica Criar(string nome, string descricao,
        bool cumprida = false, string? textoCumprimento = null, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new ProfeciaBiblica(Guid.NewGuid(), nome, descricao,
            cumprida, textoCumprimento, elementoCentral);
    }
}
