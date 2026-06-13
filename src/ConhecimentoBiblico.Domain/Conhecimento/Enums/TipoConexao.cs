namespace ConhecimentoBiblico.Domain.Conhecimento.Enums;

public enum TipoConexao
{
    // Tipologias cristocêntricas
    ApontaParaCristo = 1,
    PrefiguraCristo = 2,
    SimbolizaCristo = 3,
    CumpridoPorCristo = 4,

    // Ensino direto de Jesus
    EnsinadoPorJesus = 5,
    CitadoPorJesus = 6,

    // Explicações apostólicas — genérico (Destino define o autor)
    ExplicadoPor = 7,

    // Proféticas
    Profecia = 8,
    CumprimentoDeProfecia = 9,

    // Contextuais
    ContextoHistorico = 10,
    PassagemParalela = 11,
    RelacionadoAoTema = 12,
    RelacionadoAoPersonagem = 13,
    RelacionadoAoEvento = 14,
    RelacionadoAoLocal = 15
}
