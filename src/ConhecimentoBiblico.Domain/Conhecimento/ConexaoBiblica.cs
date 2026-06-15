using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class ConexaoBiblica
{
    private readonly List<FonteBiblica> _fontesBiblicas = new();

    private ConexaoBiblica()
    {
    }

    private ConexaoBiblica(
        Guid id,
        Guid origemId,
        Guid destinoId,
        TipoConexao tipoConexao,
        string explicacao)
    {
        Id = id;
        OrigemId = origemId;
        DestinoId = destinoId;
        TipoConexao = tipoConexao;
        Explicacao = explicacao;
    }

    public Guid Id { get; private set; }

    public Guid OrigemId { get; private set; }

    public Guid DestinoId { get; private set; }

    public ElementoBiblico? Origem { get; private set; }

    public ElementoBiblico? Destino { get; private set; }

    public TipoConexao TipoConexao { get; private set; }

    public string Explicacao { get; private set; } = string.Empty;

    /// <summary>
    /// Escala de relevância da conexão.
    /// 1 = Baixa relevância
    /// 10 = Relevância máxima
    /// </summary>
    public int Relevancia { get; private set; }

    public IReadOnlyCollection<FonteBiblica> FontesBiblicas =>
        _fontesBiblicas.AsReadOnly();

    public static ConexaoBiblica Criar(
        Guid origemId,
        Guid destinoId,
        TipoConexao tipoConexao,
        string explicacao,
        int relevancia = 5)
    {
        if (origemId == Guid.Empty)
            throw new ArgumentException("Origem inválida.", nameof(origemId));

        if (destinoId == Guid.Empty)
            throw new ArgumentException("Destino inválido.", nameof(destinoId));

        if (origemId == destinoId)
            throw new ArgumentException(
                "Origem e Destino não podem ser o mesmo elemento.");

        if (string.IsNullOrWhiteSpace(explicacao))
            throw new ArgumentException(
                "Explicação é obrigatória.",
                nameof(explicacao));

        explicacao = explicacao.Trim();

        if (explicacao.Length < 10)
            throw new ArgumentException(
                "A explicação deve possuir no mínimo 10 caracteres.");

        if (explicacao.Length > 2000)
            throw new ArgumentException(
                "A explicação deve possuir no máximo 2000 caracteres.");

        if (relevancia < 1 || relevancia > 10)
            throw new ArgumentOutOfRangeException(
                nameof(relevancia),
                "A relevância deve estar entre 1 e 10.");

        return new ConexaoBiblica(
            Guid.NewGuid(),
            origemId,
            destinoId,
            tipoConexao,
            explicacao)
        {
            Relevancia = relevancia
        };
    }

    public void AdicionarFonte(FonteBiblica fonte)
    {
        ArgumentNullException.ThrowIfNull(fonte);
        if (_fontesBiblicas.Any(x => x.Referencia == fonte.Referencia))
            return;
        _fontesBiblicas.Add(fonte);
    }

    public void AdicionarFonte(string referencia, string? textoVersiculo = null)
    {
        if (string.IsNullOrWhiteSpace(referencia))
            throw new ArgumentException("Referência é obrigatória.", nameof(referencia));
        AdicionarFonte(new FonteBiblica(referencia, textoVersiculo));
    }

    public void AtualizarExplicacao(string explicacao)
    {
        if (string.IsNullOrWhiteSpace(explicacao))
            throw new ArgumentException(
                "Explicação é obrigatória.",
                nameof(explicacao));

        explicacao = explicacao.Trim();

        if (explicacao.Length < 10)
            throw new ArgumentException(
                "A explicação deve possuir no mínimo 10 caracteres.");

        if (explicacao.Length > 2000)
            throw new ArgumentException(
                "A explicação deve possuir no máximo 2000 caracteres.");

        Explicacao = explicacao;
    }

    public void AlterarRelevancia(int relevancia)
    {
        if (relevancia < 1 || relevancia > 10)
            throw new ArgumentOutOfRangeException(
                nameof(relevancia),
                "A relevância deve estar entre 1 e 10.");

        Relevancia = relevancia;
    }
}