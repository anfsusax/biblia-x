using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class PassagemBiblica : ElementoBiblico
{
    private PassagemBiblica() { }

    private PassagemBiblica(Guid id, string nome, string descricao,
        string livro, int capitulo, int versiculoInicial, int? versiculoFinal,
        string textoResumo, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Passagem, elementoCentral)
    {
        Livro = livro;
        Capitulo = capitulo;
        VersiculoInicial = versiculoInicial;
        VersiculoFinal = versiculoFinal;
        TextoResumo = textoResumo;
    }

    public string Livro { get; private set; } = string.Empty;
    public int Capitulo { get; private set; }
    public int VersiculoInicial { get; private set; }
    public int? VersiculoFinal { get; private set; }
    public string TextoResumo { get; private set; } = string.Empty;

    public static PassagemBiblica Criar(string nome, string descricao,
        string livro, int capitulo, int versiculoInicial, int? versiculoFinal = null,
        string textoResumo = "", bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));
        if (string.IsNullOrWhiteSpace(livro))
            throw new ArgumentException("Livro é obrigatório.", nameof(livro));
        if (capitulo <= 0)
            throw new ArgumentException("Capítulo deve ser maior que zero.", nameof(capitulo));
        if (versiculoInicial <= 0)
            throw new ArgumentException("Versículo inicial deve ser maior que zero.", nameof(versiculoInicial));

        return new PassagemBiblica(Guid.NewGuid(), nome, descricao,
            livro, capitulo, versiculoInicial, versiculoFinal, textoResumo, elementoCentral);
    }
}
