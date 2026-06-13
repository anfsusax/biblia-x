using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class PersonagemBiblico : ElementoBiblico
{
    private PersonagemBiblico() { }

    private PersonagemBiblico(Guid id, string nome, string descricao,
        string periodoHistorico, string ocupacao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Personagem, elementoCentral)
    {
        PeriodoHistorico = periodoHistorico;
        Ocupacao = ocupacao;
    }

    public string PeriodoHistorico { get; private set; } = string.Empty;
    public string Ocupacao { get; private set; } = string.Empty;

    public static PersonagemBiblico Criar(string nome, string descricao,
        string periodoHistorico, string ocupacao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new PersonagemBiblico(Guid.NewGuid(), nome, descricao,
            periodoHistorico, ocupacao, elementoCentral);
    }
}
