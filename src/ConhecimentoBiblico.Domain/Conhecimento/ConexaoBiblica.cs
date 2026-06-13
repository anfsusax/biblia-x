using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class ConexaoBiblica
{
    private readonly List<FonteBiblica> _fontesBiblicas = new();

    private ConexaoBiblica() { }

    private ConexaoBiblica(Guid id, Guid origemId, Guid destinoId,
        TipoConexao tipoConexao, string explicacao)
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
    public IReadOnlyCollection<FonteBiblica> FontesBiblicas => _fontesBiblicas.AsReadOnly();

    public static ConexaoBiblica Criar(Guid origemId, Guid destinoId,
        TipoConexao tipoConexao, string explicacao)
    {
        if (origemId == destinoId)
            throw new ArgumentException("Origem e Destino não podem ser o mesmo elemento.");
        if (string.IsNullOrWhiteSpace(explicacao))
            throw new ArgumentException("Explicação é obrigatória.", nameof(explicacao));

        return new ConexaoBiblica(Guid.NewGuid(), origemId, destinoId, tipoConexao, explicacao);
    }

    public void AdicionarFonte(string referencia, string? textoVersiculo = null)
    {
        if (string.IsNullOrWhiteSpace(referencia))
            throw new ArgumentException("Referência é obrigatória.", nameof(referencia));

        _fontesBiblicas.Add(new FonteBiblica(referencia, textoVersiculo));
    }
}
