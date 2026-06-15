namespace ConhecimentoBiblico.Infrastructure.Data.Seed;

public static class DadosIniciais
{
    // PersonagemBiblico
    public static readonly Guid IdJesus        = new("11111111-1111-1111-1111-111111111111");
    public static readonly Guid IdPaulo        = new("22222222-2222-2222-2222-222222222222");
    public static readonly Guid IdPedro        = new("33333333-3333-3333-3333-333333333333");
    public static readonly Guid IdJoao         = new("44444444-4444-4444-4444-444444444444");
    public static readonly Guid IdJonas        = new("55555555-5555-5555-5555-555555555555");
    public static readonly Guid IdMoises       = new("66666666-6666-6666-6666-666666666666");
    public static readonly Guid IdDavi         = new("77777777-7777-7777-7777-777777777777");
    public static readonly Guid IdJoaoBatista  = new("88888888-8888-8888-8888-888888888888");

    // EventoBiblico — Capítulo 04: Batismo e Deserto
    public static readonly Guid IdEventoBatismo          = new("e0400001-e040-e040-e040-e04000000001");
    public static readonly Guid IdEventoTentacaoDeserto  = new("e0400002-e040-e040-e040-e04000000002");

    // ConexaoBiblica — Capítulo 04
    public static readonly Guid IdConexaoBatismoJesus        = new("cb400001-cb40-cb40-cb40-cb4000000001");
    public static readonly Guid IdConexaoBatismoJoaoBatista  = new("cb400002-cb40-cb40-cb40-cb4000000002");
    public static readonly Guid IdConexaoBatismoFe           = new("cb400003-cb40-cb40-cb40-cb4000000003");
    public static readonly Guid IdConexaoDesertJesus         = new("cb400004-cb40-cb40-cb40-cb4000000004");
    public static readonly Guid IdConexaoDesertFe            = new("cb400005-cb40-cb40-cb40-cb4000000005");

    // TemaBiblico
    public static readonly Guid IdTemaFe        = new("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    public static readonly Guid IdTemaCasamento = new("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    public static readonly Guid IdTemaPerdao    = new("cccccccc-cccc-cccc-cccc-cccccccccccc");
    public static readonly Guid IdTemaEsperanca = new("dddddddd-dddd-dddd-dddd-dddddddddddd");
    public static readonly Guid IdTemaSalvacao  = new("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

    // ConexaoBiblica
    public static readonly Guid IdConexaoJonasJesus            = new("f1111111-f111-f111-f111-f11111111111");
    public static readonly Guid IdConexaoMoisesJesus           = new("f2222222-f222-f222-f222-f22222222222");
    public static readonly Guid IdConexaoDaviJesus             = new("f3333333-f333-f333-f333-f33333333333");
    public static readonly Guid IdConexaoJonasCitadoPorJesus   = new("f4444444-f444-f444-f444-f44444444444");
    public static readonly Guid IdConexaoCasamentoPaulo        = new("f5555555-f555-f555-f555-f55555555555");
    public static readonly Guid IdConexaoPerdaoPaulo           = new("f6666666-f666-f666-f666-f66666666666");

    // PerguntaBiblica
    public static readonly Guid IdPerguntaJonas     = new("e1111111-e111-e111-e111-e11111111111");
    public static readonly Guid IdPerguntaCasamento = new("e2222222-e222-e222-e222-e22222222222");
}
