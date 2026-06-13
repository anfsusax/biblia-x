# ConhecimentoBiblico — Plano de Implementação

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Criar a estrutura inicial do Knowledge Graph bíblico cristocêntrico: solução .NET 9, domínio rico com TPH, mapeamentos EF Core e seed de dados.

**Architecture:** Clean Architecture com dois namespaces de domínio — `Conhecimento` (grafo: ElementoBiblico + subclasses + ConexaoBiblica) e `Ensino` (PerguntaBiblica + ReflexaoBiblica). TPH mapeia 8 subclasses de ElementoBiblico em uma única tabela. ConexaoBiblica conecta dois elementos com TipoConexao semântico e FontesBiblicas (OwnsMany). PerguntaBiblica aponta para nós do grafo sem duplicar conteúdo.

**Tech Stack:** .NET 9 · ASP.NET Core 9 · Blazor Web App · Entity Framework Core 9 · SQL Server (localdb) · xUnit

---

## Mapa de Arquivos

```
ConhecimentoBiblico.sln

src/ConhecimentoBiblico.Domain/
  Conhecimento/
    Enums/
      TipoElementoBiblico.cs
      TipoConexao.cs
      Testamento.cs
    ElementoBiblico.cs
    PersonagemBiblico.cs
    TemaBiblico.cs
    EventoBiblico.cs
    PassagemBiblica.cs
    ProfeciaBiblica.cs
    ParabolaBiblica.cs
    LivroBiblico.cs
    LocalBiblico.cs
    ConexaoBiblica.cs
    FonteBiblica.cs

  Ensino/
    PerguntaBiblica.cs
    ReflexaoBiblica.cs

src/ConhecimentoBiblico.Application/
  (placeholder — apenas o projeto com referência ao Domain)

src/ConhecimentoBiblico.Infrastructure/
  Data/
    ConhecimentoBiblicoDbContext.cs
    Configuracoes/
      Conhecimento/
        ElementoBiblicoConfiguracao.cs
        PersonagemBiblicoConfiguracao.cs
        PassagemBiblicaConfiguracao.cs
        ProfeciaBiblicaConfiguracao.cs
        ParabolaBiblicaConfiguracao.cs
        LivroBiblicoConfiguracao.cs
        LocalBiblicoConfiguracao.cs
        ConexaoBiblicaConfiguracao.cs
      Ensino/
        PerguntaBiblicaConfiguracao.cs
        ReflexaoBiblicaConfiguracao.cs
    Seed/
      DadosIniciais.cs

src/ConhecimentoBiblico.Web/
  Program.cs  (modificado — registra DbContext)
  appsettings.json  (modificado — connection string)

tests/ConhecimentoBiblico.UnitTests/
  Conhecimento/
    PersonagemBiblicoTestes.cs
    ConexaoBiblicaTestes.cs
  Ensino/
    PerguntaBiblicaTestes.cs
```

---

## Task 1: Criar estrutura da solução

**Files:**
- Create: `ConhecimentoBiblico.sln`
- Create: `src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj`
- Create: `src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj`
- Create: `src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj`
- Create: `src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj`
- Create: `tests/ConhecimentoBiblico.UnitTests/ConhecimentoBiblico.UnitTests.csproj`

- [ ] **Step 1: Criar solution e projetos**

```powershell
cd C:\proj\biblia-x
dotnet new sln -n ConhecimentoBiblico
dotnet new classlib -n ConhecimentoBiblico.Domain -o src/ConhecimentoBiblico.Domain --framework net9.0
dotnet new classlib -n ConhecimentoBiblico.Application -o src/ConhecimentoBiblico.Application --framework net9.0
dotnet new classlib -n ConhecimentoBiblico.Infrastructure -o src/ConhecimentoBiblico.Infrastructure --framework net9.0
dotnet new blazor -n ConhecimentoBiblico.Web -o src/ConhecimentoBiblico.Web
dotnet new xunit -n ConhecimentoBiblico.UnitTests -o tests/ConhecimentoBiblico.UnitTests --framework net9.0
```

- [ ] **Step 2: Adicionar projetos à solution**

```powershell
dotnet sln add src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj
dotnet sln add src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj
dotnet sln add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj
dotnet sln add src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj
dotnet sln add tests/ConhecimentoBiblico.UnitTests/ConhecimentoBiblico.UnitTests.csproj
```

- [ ] **Step 3: Configurar referências entre projetos**

```powershell
dotnet add src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj reference src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj

dotnet add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj reference src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj
dotnet add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj reference src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj

dotnet add src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj reference src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj
dotnet add src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj reference src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj

dotnet add tests/ConhecimentoBiblico.UnitTests/ConhecimentoBiblico.UnitTests.csproj reference src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj
dotnet add tests/ConhecimentoBiblico.UnitTests/ConhecimentoBiblico.UnitTests.csproj reference src/ConhecimentoBiblico.Application/ConhecimentoBiblico.Application.csproj
```

- [ ] **Step 4: Adicionar pacotes NuGet ao Infrastructure**

```powershell
dotnet add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
```

- [ ] **Step 5: Remover arquivos gerados desnecessários**

```powershell
Remove-Item src/ConhecimentoBiblico.Domain/Class1.cs
Remove-Item src/ConhecimentoBiblico.Application/Class1.cs
Remove-Item src/ConhecimentoBiblico.Infrastructure/Class1.cs
Remove-Item tests/ConhecimentoBiblico.UnitTests/UnitTest1.cs
```

- [ ] **Step 6: Verificar build inicial**

```powershell
dotnet build
```

Esperado: `Build succeeded` sem erros.

- [ ] **Step 7: Commit**

```powershell
git init
git add .
git commit -m "chore: criar estrutura inicial da solucao ConhecimentoBiblico"
```

---

## Task 2: Enums do domínio

**Files:**
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/TipoElementoBiblico.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/TipoConexao.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/Testamento.cs`
- Test: `tests/ConhecimentoBiblico.UnitTests/Conhecimento/EnumsTestes.cs`

- [ ] **Step 1: Criar TipoElementoBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/TipoElementoBiblico.cs`:

```csharp
namespace ConhecimentoBiblico.Domain.Conhecimento.Enums;

public enum TipoElementoBiblico
{
    Personagem = 1,
    Tema = 2,
    Evento = 3,
    Passagem = 4,
    Profecia = 5,
    Parabola = 6,
    Livro = 7,
    Local = 8
}
```

- [ ] **Step 2: Criar TipoConexao**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/TipoConexao.cs`:

```csharp
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
```

- [ ] **Step 3: Criar Testamento**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/Enums/Testamento.cs`:

```csharp
namespace ConhecimentoBiblico.Domain.Conhecimento.Enums;

public enum Testamento
{
    Antigo = 1,
    Novo = 2
}
```

- [ ] **Step 4: Escrever e executar teste de verificação dos enums**

Crie `tests/ConhecimentoBiblico.UnitTests/Conhecimento/EnumsTestes.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class EnumsTestes
{
    [Fact]
    public void TipoElementoBiblico_DeveTerOitoTipos()
    {
        var valores = Enum.GetValues<TipoElementoBiblico>();
        Assert.Equal(8, valores.Length);
    }

    [Fact]
    public void TipoConexao_DeveConterPrefiguraCristo()
    {
        Assert.True(Enum.IsDefined(typeof(TipoConexao), TipoConexao.PrefiguraCristo));
    }

    [Fact]
    public void TipoConexao_DeveConterExplicadoPor_SemVariantesEspecificas()
    {
        Assert.True(Enum.IsDefined(typeof(TipoConexao), TipoConexao.ExplicadoPor));
        Assert.False(Enum.GetNames<TipoConexao>().Any(n => n.StartsWith("ExplicadoPor") && n != "ExplicadoPor"));
    }

    [Fact]
    public void Testamento_DeveConterAntigoENovo()
    {
        Assert.True(Enum.IsDefined(typeof(Testamento), Testamento.Antigo));
        Assert.True(Enum.IsDefined(typeof(Testamento), Testamento.Novo));
    }
}
```

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~EnumsTestes"
```

Esperado: 4 testes passando.

- [ ] **Step 5: Commit**

```powershell
git add src/ConhecimentoBiblico.Domain/Conhecimento/Enums/ tests/ConhecimentoBiblico.UnitTests/Conhecimento/EnumsTestes.cs
git commit -m "feat: adicionar enums do dominio TipoElementoBiblico, TipoConexao e Testamento"
```

---

## Task 3: ElementoBiblico abstrata e PersonagemBiblico

**Files:**
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/ElementoBiblico.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/PersonagemBiblico.cs`
- Test: `tests/ConhecimentoBiblico.UnitTests/Conhecimento/PersonagemBiblicoTestes.cs`

- [ ] **Step 1: Escrever testes para PersonagemBiblico (TDD)**

Crie `tests/ConhecimentoBiblico.UnitTests/Conhecimento/PersonagemBiblicoTestes.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class PersonagemBiblicoTestes
{
    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarPersonagem()
    {
        var personagem = PersonagemBiblico.Criar("Jesus", "Filho de Deus", "Século I d.C.", "Messias");

        Assert.NotNull(personagem);
        Assert.Equal("Jesus", personagem.Nome);
        Assert.Equal(TipoElementoBiblico.Personagem, personagem.TipoElemento);
        Assert.NotEqual(Guid.Empty, personagem.Id);
    }

    [Fact]
    public void Criar_ComElementoCentralTrue_DeveMarcarComoElementoCentral()
    {
        var personagem = PersonagemBiblico.Criar("Jesus", "Filho de Deus", "Século I d.C.", "Messias", elementoCentral: true);

        Assert.True(personagem.ElementoCentral);
    }

    [Fact]
    public void Criar_ComNomeVazio_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PersonagemBiblico.Criar("", "Descrição", "Século I d.C.", "Ocupação"));

        Assert.Contains("Nome", ex.Message);
    }

    [Fact]
    public void Criar_ComDescricaoVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PersonagemBiblico.Criar("Jesus", "", "Século I d.C.", "Messias"));

        Assert.Contains("Descrição", ex.Message);
    }

    [Fact]
    public void Criar_PorPadrao_NaoDeveSerElementoCentral()
    {
        var personagem = PersonagemBiblico.Criar("Jonas", "Profeta", "Século VIII a.C.", "Profeta");

        Assert.False(personagem.ElementoCentral);
    }
}
```

- [ ] **Step 2: Executar testes — devem falhar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~PersonagemBiblicoTestes"
```

Esperado: falha com erro de compilação (tipos não existem ainda).

- [ ] **Step 3: Implementar ElementoBiblico abstrata**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/ElementoBiblico.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public abstract class ElementoBiblico
{
    protected ElementoBiblico() { }

    protected ElementoBiblico(Guid id, string nome, string descricao,
        TipoElementoBiblico tipoElemento, bool elementoCentral)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        TipoElemento = tipoElemento;
        ElementoCentral = elementoCentral;
    }

    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public TipoElementoBiblico TipoElemento { get; private set; }
    public bool ElementoCentral { get; private set; }
}
```

- [ ] **Step 4: Implementar PersonagemBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/PersonagemBiblico.cs`:

```csharp
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
```

- [ ] **Step 5: Executar testes — devem passar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~PersonagemBiblicoTestes"
```

Esperado: 5 testes passando.

- [ ] **Step 6: Commit**

```powershell
git add src/ConhecimentoBiblico.Domain/Conhecimento/ElementoBiblico.cs src/ConhecimentoBiblico.Domain/Conhecimento/PersonagemBiblico.cs tests/ConhecimentoBiblico.UnitTests/Conhecimento/PersonagemBiblicoTestes.cs
git commit -m "feat: adicionar ElementoBiblico abstrata e PersonagemBiblico com TDD"
```

---

## Task 4: Subclasses restantes de ElementoBiblico

**Files:**
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/TemaBiblico.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/EventoBiblico.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/PassagemBiblica.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/ProfeciaBiblica.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/ParabolaBiblica.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/LivroBiblico.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/LocalBiblico.cs`

- [ ] **Step 1: Criar TemaBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/TemaBiblico.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class TemaBiblico : ElementoBiblico
{
    private TemaBiblico() { }

    private TemaBiblico(Guid id, string nome, string descricao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Tema, elementoCentral) { }

    public static TemaBiblico Criar(string nome, string descricao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new TemaBiblico(Guid.NewGuid(), nome, descricao, elementoCentral);
    }
}
```

- [ ] **Step 2: Criar EventoBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/EventoBiblico.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class EventoBiblico : ElementoBiblico
{
    private EventoBiblico() { }

    private EventoBiblico(Guid id, string nome, string descricao, bool elementoCentral)
        : base(id, nome, descricao, TipoElementoBiblico.Evento, elementoCentral) { }

    public static EventoBiblico Criar(string nome, string descricao, bool elementoCentral = false)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        return new EventoBiblico(Guid.NewGuid(), nome, descricao, elementoCentral);
    }
}
```

- [ ] **Step 3: Criar PassagemBiblica**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/PassagemBiblica.cs`:

```csharp
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
```

- [ ] **Step 4: Criar ProfeciaBiblica**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/ProfeciaBiblica.cs`:

```csharp
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
```

- [ ] **Step 5: Criar ParabolaBiblica**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/ParabolaBiblica.cs`:

```csharp
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
```

- [ ] **Step 6: Criar LivroBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/LivroBiblico.cs`:

```csharp
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
```

- [ ] **Step 7: Criar LocalBiblico**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/LocalBiblico.cs`:

```csharp
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
```

- [ ] **Step 8: Build para verificar todas as subclasses**

```powershell
dotnet build src/ConhecimentoBiblico.Domain/ConhecimentoBiblico.Domain.csproj
```

Esperado: `Build succeeded` sem warnings relevantes.

- [ ] **Step 9: Commit**

```powershell
git add src/ConhecimentoBiblico.Domain/Conhecimento/
git commit -m "feat: adicionar subclasses de ElementoBiblico (Tema, Evento, Passagem, Profecia, Parabola, Livro, Local)"
```

---

## Task 5: ConexaoBiblica e FonteBiblica

**Files:**
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/FonteBiblica.cs`
- Create: `src/ConhecimentoBiblico.Domain/Conhecimento/ConexaoBiblica.cs`
- Test: `tests/ConhecimentoBiblico.UnitTests/Conhecimento/ConexaoBiblicaTestes.cs`

- [ ] **Step 1: Escrever testes para ConexaoBiblica (TDD)**

Crie `tests/ConhecimentoBiblico.UnitTests/Conhecimento/ConexaoBiblicaTestes.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;

namespace ConhecimentoBiblico.UnitTests.Conhecimento;

public class ConexaoBiblicaTestes
{
    private static readonly Guid _origemId = Guid.NewGuid();
    private static readonly Guid _destinoId = Guid.NewGuid();

    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarConexao()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Jonas prefigura Cristo na ressurreição.");

        Assert.NotNull(conexao);
        Assert.Equal(_origemId, conexao.OrigemId);
        Assert.Equal(_destinoId, conexao.DestinoId);
        Assert.Equal(TipoConexao.PrefiguraCristo, conexao.TipoConexao);
        Assert.NotEqual(Guid.Empty, conexao.Id);
    }

    [Fact]
    public void Criar_ComOrigemIgualDestino_DeveLancarArgumentException()
    {
        var mesmoid = Guid.NewGuid();
        var ex = Assert.Throws<ArgumentException>(() =>
            ConexaoBiblica.Criar(mesmoid, mesmoid, TipoConexao.PrefiguraCristo, "Explicação."));

        Assert.Contains("Origem e Destino", ex.Message);
    }

    [Fact]
    public void Criar_ComExplicacaoVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            ConexaoBiblica.Criar(_origemId, _destinoId, TipoConexao.PrefiguraCristo, ""));

        Assert.Contains("Explicação", ex.Message);
    }

    [Fact]
    public void AdicionarFonte_ComReferenciaValida_DeveAdicionarFonte()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        conexao.AdicionarFonte("Mateus 12:40", "Pois assim como Jonas...");

        Assert.Single(conexao.FontesBiblicas);
        Assert.Equal("Mateus 12:40", conexao.FontesBiblicas.First().Referencia);
    }

    [Fact]
    public void AdicionarFonte_ComReferenciaVazia_DeveLancarArgumentException()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        Assert.Throws<ArgumentException>(() => conexao.AdicionarFonte(""));
    }

    [Fact]
    public void AdicionarFonte_SemTextoVersiculo_DevePermitirNulo()
    {
        var conexao = ConexaoBiblica.Criar(_origemId, _destinoId,
            TipoConexao.PrefiguraCristo, "Explicação.");

        conexao.AdicionarFonte("João 5:46");

        Assert.Null(conexao.FontesBiblicas.First().TextoVersiculo);
    }
}
```

- [ ] **Step 2: Executar testes — devem falhar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~ConexaoBiblicaTestes"
```

Esperado: falha com erro de compilação.

- [ ] **Step 3: Implementar FonteBiblica (Value Object)**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/FonteBiblica.cs`:

```csharp
namespace ConhecimentoBiblico.Domain.Conhecimento;

public sealed class FonteBiblica
{
    private FonteBiblica() { }

    public FonteBiblica(string referencia, string? textoVersiculo = null)
    {
        Referencia = referencia;
        TextoVersiculo = textoVersiculo;
    }

    public string Referencia { get; private set; } = string.Empty;
    public string? TextoVersiculo { get; private set; }

    public override bool Equals(object? obj) =>
        obj is FonteBiblica other && Referencia == other.Referencia;

    public override int GetHashCode() => Referencia.GetHashCode();
}
```

- [ ] **Step 4: Implementar ConexaoBiblica**

Crie `src/ConhecimentoBiblico.Domain/Conhecimento/ConexaoBiblica.cs`:

```csharp
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
```

- [ ] **Step 5: Executar testes — devem passar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~ConexaoBiblicaTestes"
```

Esperado: 6 testes passando.

- [ ] **Step 6: Commit**

```powershell
git add src/ConhecimentoBiblico.Domain/Conhecimento/FonteBiblica.cs src/ConhecimentoBiblico.Domain/Conhecimento/ConexaoBiblica.cs tests/ConhecimentoBiblico.UnitTests/Conhecimento/ConexaoBiblicaTestes.cs
git commit -m "feat: adicionar ConexaoBiblica e FonteBiblica com TDD"
```

---

## Task 6: PerguntaBiblica e ReflexaoBiblica

**Files:**
- Create: `src/ConhecimentoBiblico.Domain/Ensino/ReflexaoBiblica.cs`
- Create: `src/ConhecimentoBiblico.Domain/Ensino/PerguntaBiblica.cs`
- Test: `tests/ConhecimentoBiblico.UnitTests/Ensino/PerguntaBiblicaTestes.cs`

- [ ] **Step 1: Escrever testes para PerguntaBiblica (TDD)**

Crie `tests/ConhecimentoBiblico.UnitTests/Ensino/PerguntaBiblicaTestes.cs`:

```csharp
using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Conhecimento;

namespace ConhecimentoBiblico.UnitTests.Ensino;

public class PerguntaBiblicaTestes
{
    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarPergunta()
    {
        var pergunta = PerguntaBiblica.Criar("Casamento Cristão", "O que é casamento?");

        Assert.NotNull(pergunta);
        Assert.Equal("O que é casamento?", pergunta.Pergunta);
        Assert.Equal("Casamento Cristão", pergunta.Titulo);
        Assert.NotEqual(Guid.Empty, pergunta.Id);
    }

    [Fact]
    public void Criar_ComPerguntaVazia_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PerguntaBiblica.Criar("Título", ""));

        Assert.Contains("Pergunta", ex.Message);
    }

    [Fact]
    public void Criar_ComTituloVazio_DeveLancarArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            PerguntaBiblica.Criar("", "Pergunta válida?"));

        Assert.Contains("Título", ex.Message);
    }

    [Fact]
    public void Criar_DeveTerColecaoDeElementosVazia()
    {
        var pergunta = PerguntaBiblica.Criar("Título", "Pergunta?");

        Assert.Empty(pergunta.ElementosRelacionados);
    }

    [Fact]
    public void Criar_DeveTerColecaoDeReflexoesVazia()
    {
        var pergunta = PerguntaBiblica.Criar("Título", "Pergunta?");

        Assert.Empty(pergunta.Reflexoes);
    }
}
```

- [ ] **Step 2: Executar testes — devem falhar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~PerguntaBiblicaTestes"
```

Esperado: falha com erro de compilação.

- [ ] **Step 3: Implementar ReflexaoBiblica**

Crie `src/ConhecimentoBiblico.Domain/Ensino/ReflexaoBiblica.cs`:

```csharp
namespace ConhecimentoBiblico.Domain.Ensino;

public sealed class ReflexaoBiblica
{
    private ReflexaoBiblica() { }

    private ReflexaoBiblica(Guid id, Guid perguntaBiblicaId, string titulo,
        string explicacao, string aplicacaoPratica, string perguntaReflexao)
    {
        Id = id;
        PerguntaBiblicaId = perguntaBiblicaId;
        Titulo = titulo;
        Explicacao = explicacao;
        AplicacaoPratica = aplicacaoPratica;
        PerguntaReflexao = perguntaReflexao;
    }

    public Guid Id { get; private set; }
    public Guid PerguntaBiblicaId { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Explicacao { get; private set; } = string.Empty;
    public string AplicacaoPratica { get; private set; } = string.Empty;
    public string PerguntaReflexao { get; private set; } = string.Empty;

    public static ReflexaoBiblica Criar(Guid perguntaBiblicaId, string titulo,
        string explicacao, string aplicacaoPratica, string perguntaReflexao)
    {
        if (perguntaBiblicaId == Guid.Empty)
            throw new ArgumentException("PerguntaBiblicaId é obrigatório.", nameof(perguntaBiblicaId));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título é obrigatório.", nameof(titulo));

        return new ReflexaoBiblica(Guid.NewGuid(), perguntaBiblicaId, titulo,
            explicacao, aplicacaoPratica, perguntaReflexao);
    }
}
```

- [ ] **Step 4: Implementar PerguntaBiblica**

Crie `src/ConhecimentoBiblico.Domain/Ensino/PerguntaBiblica.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;

namespace ConhecimentoBiblico.Domain.Ensino;

public sealed class PerguntaBiblica
{
    private readonly List<ElementoBiblico> _elementosRelacionados = new();
    private readonly List<ReflexaoBiblica> _reflexoes = new();

    private PerguntaBiblica() { }

    private PerguntaBiblica(Guid id, string titulo, string pergunta)
    {
        Id = id;
        Titulo = titulo;
        Pergunta = pergunta;
    }

    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Pergunta { get; private set; } = string.Empty;
    public IReadOnlyCollection<ElementoBiblico> ElementosRelacionados => _elementosRelacionados.AsReadOnly();
    public IReadOnlyCollection<ReflexaoBiblica> Reflexoes => _reflexoes.AsReadOnly();

    public static PerguntaBiblica Criar(string titulo, string pergunta)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(pergunta))
            throw new ArgumentException("Pergunta é obrigatória.", nameof(pergunta));

        return new PerguntaBiblica(Guid.NewGuid(), titulo, pergunta);
    }

    public void AdicionarElementoRelacionado(ElementoBiblico elemento)
    {
        ArgumentNullException.ThrowIfNull(elemento);
        if (!_elementosRelacionados.Any(e => e.Id == elemento.Id))
            _elementosRelacionados.Add(elemento);
    }

    public void AdicionarReflexao(ReflexaoBiblica reflexao)
    {
        ArgumentNullException.ThrowIfNull(reflexao);
        _reflexoes.Add(reflexao);
    }
}
```

- [ ] **Step 5: Executar testes — devem passar**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests --filter "FullyQualifiedName~PerguntaBiblicaTestes"
```

Esperado: 5 testes passando.

- [ ] **Step 6: Executar todos os testes do Domain**

```powershell
dotnet test tests/ConhecimentoBiblico.UnitTests
```

Esperado: 20 testes passando, 0 falhas.

- [ ] **Step 7: Commit**

```powershell
git add src/ConhecimentoBiblico.Domain/Ensino/ tests/ConhecimentoBiblico.UnitTests/Ensino/
git commit -m "feat: adicionar PerguntaBiblica e ReflexaoBiblica com TDD"
```

---

## Task 7: DbContext e configurações EF Core

**Files:**
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/ConhecimentoBiblicoDbContext.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ElementoBiblicoConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/PersonagemBiblicoConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/PassagemBiblicaConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ProfeciaBiblicaConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ParabolaBiblicaConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/LivroBiblicoConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/LocalBiblicoConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ConexaoBiblicaConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/PerguntaBiblicaConfiguracao.cs`
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/ReflexaoBiblicaConfiguracao.cs`

- [ ] **Step 1: Criar DbContext**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/ConhecimentoBiblicoDbContext.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Ensino;
using Microsoft.EntityFrameworkCore;

namespace ConhecimentoBiblico.Infrastructure.Data;

public class ConhecimentoBiblicoDbContext : DbContext
{
    public ConhecimentoBiblicoDbContext(DbContextOptions<ConhecimentoBiblicoDbContext> options)
        : base(options) { }

    public DbSet<ElementoBiblico> ElementosBiblicos => Set<ElementoBiblico>();
    public DbSet<ConexaoBiblica> ConexoesBiblicas => Set<ConexaoBiblica>();
    public DbSet<PerguntaBiblica> PerguntasBiblicas => Set<PerguntaBiblica>();
    public DbSet<ReflexaoBiblica> ReflexoesBiblicas => Set<ReflexaoBiblica>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConhecimentoBiblicoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
```

- [ ] **Step 2: Criar configuração base de ElementoBiblico (TPH)**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ElementoBiblicoConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ElementoBiblicoConfiguracao : IEntityTypeConfiguration<ElementoBiblico>
{
    public void Configure(EntityTypeBuilder<ElementoBiblico> builder)
    {
        builder.ToTable("ElementosBiblicos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Descricao)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(e => e.ElementoCentral)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasDiscriminator<TipoElementoBiblico>(e => e.TipoElemento)
            .HasValue<PersonagemBiblico>(TipoElementoBiblico.Personagem)
            .HasValue<TemaBiblico>(TipoElementoBiblico.Tema)
            .HasValue<EventoBiblico>(TipoElementoBiblico.Evento)
            .HasValue<PassagemBiblica>(TipoElementoBiblico.Passagem)
            .HasValue<ProfeciaBiblica>(TipoElementoBiblico.Profecia)
            .HasValue<ParabolaBiblica>(TipoElementoBiblico.Parabola)
            .HasValue<LivroBiblico>(TipoElementoBiblico.Livro)
            .HasValue<LocalBiblico>(TipoElementoBiblico.Local);
    }
}
```

- [ ] **Step 3: Criar configurações das subclasses com propriedades específicas**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/PersonagemBiblicoConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PersonagemBiblicoConfiguracao : IEntityTypeConfiguration<PersonagemBiblico>
{
    public void Configure(EntityTypeBuilder<PersonagemBiblico> builder)
    {
        builder.Property(p => p.PeriodoHistorico).HasMaxLength(200);
        builder.Property(p => p.Ocupacao).HasMaxLength(200);
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/PassagemBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PassagemBiblicaConfiguracao : IEntityTypeConfiguration<PassagemBiblica>
{
    public void Configure(EntityTypeBuilder<PassagemBiblica> builder)
    {
        builder.Property(p => p.Livro).HasMaxLength(100);
        builder.Property(p => p.TextoResumo).HasMaxLength(1000);
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ProfeciaBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ProfeciaBiblicaConfiguracao : IEntityTypeConfiguration<ProfeciaBiblica>
{
    public void Configure(EntityTypeBuilder<ProfeciaBiblica> builder)
    {
        builder.Property(p => p.TextoCumprimento).HasMaxLength(1000);
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ParabolaBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ParabolaBiblicaConfiguracao : IEntityTypeConfiguration<ParabolaBiblica>
{
    public void Configure(EntityTypeBuilder<ParabolaBiblica> builder)
    {
        builder.Property(p => p.LicaoCentral).HasMaxLength(500);
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/LivroBiblicoConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class LivroBiblicoConfiguracao : IEntityTypeConfiguration<LivroBiblico>
{
    public void Configure(EntityTypeBuilder<LivroBiblico> builder)
    {
        builder.Property(l => l.Testamento).IsRequired();
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/LocalBiblicoConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class LocalBiblicoConfiguracao : IEntityTypeConfiguration<LocalBiblico>
{
    public void Configure(EntityTypeBuilder<LocalBiblico> builder)
    {
        builder.Property(l => l.Regiao).HasMaxLength(200);
    }
}
```

- [ ] **Step 4: Criar configuração de ConexaoBiblica**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ConexaoBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ConexaoBiblicaConfiguracao : IEntityTypeConfiguration<ConexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ConexaoBiblica> builder)
    {
        builder.ToTable("ConexoesBiblicas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Explicacao)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.TipoConexao).IsRequired();

        builder.HasOne(c => c.Origem)
            .WithMany()
            .HasForeignKey(c => c.OrigemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Destino)
            .WithMany()
            .HasForeignKey(c => c.DestinoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(c => c.FontesBiblicas, fb =>
        {
            fb.ToTable("ConexaoFontesBiblicas");
            fb.Property<int>("Id").ValueGeneratedOnAdd();
            fb.HasKey("Id");
            fb.WithOwner().HasForeignKey("ConexaoBiblicaId");
            fb.Property(f => f.Referencia).IsRequired().HasMaxLength(50);
            fb.Property(f => f.TextoVersiculo).HasMaxLength(500);
        });
    }
}
```

- [ ] **Step 5: Criar configurações da camada Ensino**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/PerguntaBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Conhecimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class PerguntaBiblicaConfiguracao : IEntityTypeConfiguration<PerguntaBiblica>
{
    public void Configure(EntityTypeBuilder<PerguntaBiblica> builder)
    {
        builder.ToTable("PerguntasBiblicas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Pergunta)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(p => p.ElementosRelacionados)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PerguntaElementos",
                b => b.HasOne<ElementoBiblico>().WithMany()
                    .HasForeignKey("ElementoBiblicoId")
                    .OnDelete(DeleteBehavior.Cascade),
                b => b.HasOne<PerguntaBiblica>().WithMany()
                    .HasForeignKey("PerguntaBiblicaId")
                    .OnDelete(DeleteBehavior.Cascade),
                b => b.HasKey("PerguntaBiblicaId", "ElementoBiblicoId")
            );
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/ReflexaoBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Ensino;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class ReflexaoBiblicaConfiguracao : IEntityTypeConfiguration<ReflexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ReflexaoBiblica> builder)
    {
        builder.ToTable("ReflexoesBiblicas");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Explicacao).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.AplicacaoPratica).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.PerguntaReflexao).IsRequired().HasMaxLength(500);

        builder.HasOne<PerguntaBiblica>()
            .WithMany(p => p.Reflexoes)
            .HasForeignKey(r => r.PerguntaBiblicaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

- [ ] **Step 6: Build do Infrastructure**

```powershell
dotnet build src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj
```

Esperado: `Build succeeded`.

- [ ] **Step 7: Commit**

```powershell
git add src/ConhecimentoBiblico.Infrastructure/
git commit -m "feat: adicionar DbContext e configuracoes EF Core para todas as entidades"
```

---

## Task 8: Seed de dados iniciais

**Files:**
- Create: `src/ConhecimentoBiblico.Infrastructure/Data/Seed/DadosIniciais.cs`
- Modify: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ElementoBiblicoConfiguracao.cs`
- Modify: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/ConexaoBiblicaConfiguracao.cs`
- Modify: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/PerguntaBiblicaConfiguracao.cs`
- Modify: `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Ensino/ReflexaoBiblicaConfiguracao.cs`

- [ ] **Step 1: Criar DadosIniciais com GUIDs fixos**

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Seed/DadosIniciais.cs`:

```csharp
namespace ConhecimentoBiblico.Infrastructure.Data.Seed;

public static class DadosIniciais
{
    // PersonagemBiblico
    public static readonly Guid IdJesus   = new("11111111-1111-1111-1111-111111111111");
    public static readonly Guid IdPaulo   = new("22222222-2222-2222-2222-222222222222");
    public static readonly Guid IdPedro   = new("33333333-3333-3333-3333-333333333333");
    public static readonly Guid IdJoao    = new("44444444-4444-4444-4444-444444444444");
    public static readonly Guid IdJonas   = new("55555555-5555-5555-5555-555555555555");
    public static readonly Guid IdMoises  = new("66666666-6666-6666-6666-666666666666");
    public static readonly Guid IdDavi    = new("77777777-7777-7777-7777-777777777777");

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
```

- [ ] **Step 2: Adicionar HasData para PersonagemBiblico e TemaBiblico**

Substitua o conteúdo de `ElementoBiblicoConfiguracao.cs` — adicione o bloco de seed após a configuração do discriminador:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ElementoBiblicoConfiguracao : IEntityTypeConfiguration<ElementoBiblico>
{
    public void Configure(EntityTypeBuilder<ElementoBiblico> builder)
    {
        builder.ToTable("ElementosBiblicos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Descricao).IsRequired().HasMaxLength(2000);
        builder.Property(e => e.ElementoCentral).IsRequired().HasDefaultValue(false);

        builder.HasDiscriminator<TipoElementoBiblico>(e => e.TipoElemento)
            .HasValue<PersonagemBiblico>(TipoElementoBiblico.Personagem)
            .HasValue<TemaBiblico>(TipoElementoBiblico.Tema)
            .HasValue<EventoBiblico>(TipoElementoBiblico.Evento)
            .HasValue<PassagemBiblica>(TipoElementoBiblico.Passagem)
            .HasValue<ProfeciaBiblica>(TipoElementoBiblico.Profecia)
            .HasValue<ParabolaBiblica>(TipoElementoBiblico.Parabola)
            .HasValue<LivroBiblico>(TipoElementoBiblico.Livro)
            .HasValue<LocalBiblico>(TipoElementoBiblico.Local);
    }
}
```

Substitua `PersonagemBiblicoConfiguracao.cs` com seed incluído:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class PersonagemBiblicoConfiguracao : IEntityTypeConfiguration<PersonagemBiblico>
{
    public void Configure(EntityTypeBuilder<PersonagemBiblico> builder)
    {
        builder.Property(p => p.PeriodoHistorico).HasMaxLength(200);
        builder.Property(p => p.Ocupacao).HasMaxLength(200);

        builder.HasData(
            new { Id = DadosIniciais.IdJesus,  Nome = "Jesus",  Descricao = "Filho de Deus, o Messias prometido e cumprimento de todas as profecias.", TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = true,  PeriodoHistorico = "Século I d.C.",    Ocupacao = "Filho de Deus, Messias"     },
            new { Id = DadosIniciais.IdPaulo,  Nome = "Paulo",  Descricao = "Apóstolo dos gentios, responsável por explicar a teologia de Cristo.",     TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Teólogo"          },
            new { Id = DadosIniciais.IdPedro,  Nome = "Pedro",  Descricao = "Apóstolo e líder da Igreja primitiva.",                                     TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Pescador"         },
            new { Id = DadosIniciais.IdJoao,   Nome = "João",   Descricao = "Apóstolo e evangelista, discípulo amado de Jesus.",                         TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século I d.C.",    Ocupacao = "Apóstolo, Evangelista"      },
            new { Id = DadosIniciais.IdJonas,  Nome = "Jonas",  Descricao = "Profeta cujos 3 dias no ventre do peixe prefiguram a ressurreição de Cristo.", TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século VIII a.C.", Ocupacao = "Profeta"                   },
            new { Id = DadosIniciais.IdMoises, Nome = "Moisés", Descricao = "Profeta e legislador, mediador da antiga aliança que prefigura Cristo.",      TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século XIII a.C.", Ocupacao = "Profeta, Legislador"        },
            new { Id = DadosIniciais.IdDavi,   Nome = "Davi",   Descricao = "Rei e salmista, cujo trono eterno aponta para o reinado de Cristo.",          TipoElemento = TipoElementoBiblico.Personagem, ElementoCentral = false, PeriodoHistorico = "Século X a.C.",    Ocupacao = "Rei, Salmista"              }
        );
    }
}
```

Crie `src/ConhecimentoBiblico.Infrastructure/Data/Configuracoes/Conhecimento/TemaBiblicoConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class TemaBiblicoConfiguracao : IEntityTypeConfiguration<TemaBiblico>
{
    public void Configure(EntityTypeBuilder<TemaBiblico> builder)
    {
        builder.HasData(
            new { Id = DadosIniciais.IdTemaFe,        Nome = "Fé",        Descricao = "Confiança e crença em Deus e em Seus propósitos.",                    TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaCasamento, Nome = "Casamento", Descricao = "União entre homem e mulher que reflete a relação de Cristo e a Igreja.", TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaPerdao,    Nome = "Perdão",    Descricao = "A graça de Deus que perdoa os pecados através de Cristo.",               TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaEsperanca, Nome = "Esperança", Descricao = "A certeza das promessas de Deus cumpridas em Cristo.",                   TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false },
            new { Id = DadosIniciais.IdTemaSalvacao,  Nome = "Salvação",  Descricao = "A redenção do ser humano através do sacrifício de Jesus Cristo.",        TipoElemento = TipoElementoBiblico.Tema, ElementoCentral = false }
        );
    }
}
```

- [ ] **Step 3: Adicionar HasData para ConexaoBiblica e FontesBiblicas**

Substitua `ConexaoBiblicaConfiguracao.cs` com seed incluído:

```csharp
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Conhecimento.Enums;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Conhecimento;

public class ConexaoBiblicaConfiguracao : IEntityTypeConfiguration<ConexaoBiblica>
{
    public void Configure(EntityTypeBuilder<ConexaoBiblica> builder)
    {
        builder.ToTable("ConexoesBiblicas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Explicacao).IsRequired().HasMaxLength(2000);
        builder.Property(c => c.TipoConexao).IsRequired();

        builder.HasOne(c => c.Origem).WithMany()
            .HasForeignKey(c => c.OrigemId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Destino).WithMany()
            .HasForeignKey(c => c.DestinoId).OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(c => c.FontesBiblicas, fb =>
        {
            fb.ToTable("ConexaoFontesBiblicas");
            fb.Property<int>("Id").ValueGeneratedOnAdd();
            fb.HasKey("Id");
            fb.WithOwner().HasForeignKey("ConexaoBiblicaId");
            fb.Property(f => f.Referencia).IsRequired().HasMaxLength(50);
            fb.Property(f => f.TextoVersiculo).HasMaxLength(500);

            fb.HasData(
                new { Id = 1, ConexaoBiblicaId = DadosIniciais.IdConexaoJonasJesus,          Referencia = "Mateus 12:40",   TextoVersiculo = (string?)null },
                new { Id = 2, ConexaoBiblicaId = DadosIniciais.IdConexaoMoisesJesus,          Referencia = "João 5:46",      TextoVersiculo = (string?)null },
                new { Id = 3, ConexaoBiblicaId = DadosIniciais.IdConexaoDaviJesus,            Referencia = "Lucas 20:41-44", TextoVersiculo = (string?)null },
                new { Id = 4, ConexaoBiblicaId = DadosIniciais.IdConexaoJonasCitadoPorJesus,  Referencia = "Mateus 12:39",   TextoVersiculo = (string?)null },
                new { Id = 5, ConexaoBiblicaId = DadosIniciais.IdConexaoCasamentoPaulo,       Referencia = "Efésios 5:25-32",TextoVersiculo = (string?)null },
                new { Id = 6, ConexaoBiblicaId = DadosIniciais.IdConexaoPerdaoPaulo,          Referencia = "Efésios 4:32",   TextoVersiculo = (string?)null }
            );
        });

        builder.HasData(
            new { Id = DadosIniciais.IdConexaoJonasJesus,          OrigemId = DadosIniciais.IdJonas,         DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.PrefiguraCristo, Explicacao = "Jonas ficou 3 dias no ventre do peixe, prefigurando os 3 dias de Jesus no sepulcro e Sua ressurreição." },
            new { Id = DadosIniciais.IdConexaoMoisesJesus,          OrigemId = DadosIniciais.IdMoises,        DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.PrefiguraCristo, Explicacao = "Moisés como mediador da antiga aliança prefigura Cristo, mediador da nova e eterna aliança." },
            new { Id = DadosIniciais.IdConexaoDaviJesus,            OrigemId = DadosIniciais.IdDavi,          DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.ApontaParaCristo, Explicacao = "O trono eterno prometido a Davi aponta para o reino eterno de Jesus Cristo." },
            new { Id = DadosIniciais.IdConexaoJonasCitadoPorJesus,  OrigemId = DadosIniciais.IdJonas,         DestinoId = DadosIniciais.IdJesus,  TipoConexao = TipoConexao.CitadoPorJesus,  Explicacao = "Jesus citou Jonas explicitamente ao responder sobre o sinal do Filho do Homem (Mt 12:39-40)." },
            new { Id = DadosIniciais.IdConexaoCasamentoPaulo,        OrigemId = DadosIniciais.IdTemaCasamento, DestinoId = DadosIniciais.IdPaulo,  TipoConexao = TipoConexao.ExplicadoPor,    Explicacao = "Paulo ensina que o casamento é reflexo da relação de Cristo com a Igreja em Efésios 5:25-32." },
            new { Id = DadosIniciais.IdConexaoPerdaoPaulo,           OrigemId = DadosIniciais.IdTemaPerdao,    DestinoId = DadosIniciais.IdPaulo,  TipoConexao = TipoConexao.ExplicadoPor,    Explicacao = "Paulo explica o perdão como graça de Deus em Cristo, exortando os crentes a perdoarem uns aos outros." }
        );
    }
}
```

- [ ] **Step 4: Adicionar HasData para PerguntaBiblica com join table e ReflexaoBiblica**

Substitua `PerguntaBiblicaConfiguracao.cs`:

```csharp
using ConhecimentoBiblico.Domain.Ensino;
using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConhecimentoBiblico.Infrastructure.Data.Configuracoes.Ensino;

public class PerguntaBiblicaConfiguracao : IEntityTypeConfiguration<PerguntaBiblica>
{
    public void Configure(EntityTypeBuilder<PerguntaBiblica> builder)
    {
        builder.ToTable("PerguntasBiblicas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Pergunta).IsRequired().HasMaxLength(500);

        builder.HasMany(p => p.ElementosRelacionados)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PerguntaElementos",
                b => b.HasOne<ElementoBiblico>().WithMany()
                    .HasForeignKey("ElementoBiblicoId").OnDelete(DeleteBehavior.Cascade),
                b => b.HasOne<PerguntaBiblica>().WithMany()
                    .HasForeignKey("PerguntaBiblicaId").OnDelete(DeleteBehavior.Cascade),
                b =>
                {
                    b.HasKey("PerguntaBiblicaId", "ElementoBiblicoId");
                    b.HasData(
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaJonas,     ElementoBiblicoId = DadosIniciais.IdJonas         },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaJonas,     ElementoBiblicoId = DadosIniciais.IdJesus         },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaCasamento, ElementoBiblicoId = DadosIniciais.IdTemaCasamento },
                        new { PerguntaBiblicaId = DadosIniciais.IdPerguntaCasamento, ElementoBiblicoId = DadosIniciais.IdPaulo         }
                    );
                }
            );

        builder.HasData(
            new { Id = DadosIniciais.IdPerguntaJonas,     Titulo = "Jonas e a Ressurreição",  Pergunta = "Por que Jesus citou Jonas?" },
            new { Id = DadosIniciais.IdPerguntaCasamento, Titulo = "O Que é Casamento?",      Pergunta = "O que a Bíblia ensina sobre casamento?" }
        );
    }
}
```

- [ ] **Step 5: Build do Infrastructure com seed**

```powershell
dotnet build src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj
```

Esperado: `Build succeeded`.

- [ ] **Step 6: Commit**

```powershell
git add src/ConhecimentoBiblico.Infrastructure/
git commit -m "feat: adicionar seed inicial com personagens, temas, conexoes e perguntas"
```

---

## Task 9: Configurar Web project e registrar DbContext

**Files:**
- Modify: `src/ConhecimentoBiblico.Web/Program.cs`
- Modify: `src/ConhecimentoBiblico.Web/appsettings.json`

- [ ] **Step 1: Adicionar connection string ao appsettings.json**

Edite `src/ConhecimentoBiblico.Web/appsettings.json` para incluir:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ConhecimentoBiblico;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

- [ ] **Step 2: Registrar DbContext em Program.cs**

Abra `src/ConhecimentoBiblico.Web/Program.cs` (gerado pelo template) e adicione as duas linhas indicadas — não substitua o arquivo inteiro:

```csharp
// Adicionar no topo do arquivo, junto com os outros usings:
using ConhecimentoBiblico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

// Adicionar ANTES de builder.Build(), junto com os outros builder.Services.*:
builder.Services.AddDbContext<ConhecimentoBiblicoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

O restante do `Program.cs` permanece como gerado pelo template Blazor.

- [ ] **Step 3: Adicionar pacote EF Core Design ao Web (necessário para dotnet ef)**

```powershell
dotnet add src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj package Microsoft.EntityFrameworkCore.Design --version 9.0.0
```

- [ ] **Step 4: Build completo da solução**

```powershell
dotnet build
```

Esperado: `Build succeeded` para todos os 5 projetos.

- [ ] **Step 5: Commit**

```powershell
git add src/ConhecimentoBiblico.Web/Program.cs src/ConhecimentoBiblico.Web/appsettings.json src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj
git commit -m "feat: registrar DbContext no Web project com connection string localdb"
```

---

## Task 10: Migration inicial e verificação do banco

**Files:**
- Create: `src/ConhecimentoBiblico.Infrastructure/Migrations/` (gerado automaticamente)

- [ ] **Step 1: Verificar dotnet-ef tool e gerar migration inicial**

```powershell
# Verificar se dotnet-ef está instalado
dotnet ef --version
# Se não estiver: dotnet tool install --global dotnet-ef
```

```powershell
dotnet ef migrations add Inicial `
  --project src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj `
  --startup-project src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj
```

Esperado: criação de `Migrations/XXXXXX_Inicial.cs` e `Migrations/ConhecimentoBiblicoDbContextModelSnapshot.cs`.

- [ ] **Step 2: Verificar migration gerada**

Abra o arquivo `src/ConhecimentoBiblico.Infrastructure/Migrations/XXXXXX_Inicial.cs` e confirme a presença de:
- Tabela `ElementosBiblicos` com coluna discriminadora `TipoElemento`
- Coluna `ElementoCentral` em `ElementosBiblicos`
- Tabela `ConexoesBiblicas` com FKs `OrigemId` e `DestinoId`
- Tabela `ConexaoFontesBiblicas` com FK `ConexaoBiblicaId`
- Tabela `PerguntasBiblicas`
- Tabela `PerguntaElementos` (join table)
- Tabela `ReflexoesBiblicas` com FK `PerguntaBiblicaId`
- Seed data nas tabelas acima

- [ ] **Step 3: Aplicar migration ao banco**

```powershell
dotnet ef database update `
  --project src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj `
  --startup-project src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj
```

Esperado: `Done. Applied 1 migration(s)`.

- [ ] **Step 4: Verificar seed via dotnet-script ou query SQL**

```powershell
dotnet ef dbcontext info `
  --project src/ConhecimentoBiblico.Infrastructure/ConhecimentoBiblico.Infrastructure.csproj `
  --startup-project src/ConhecimentoBiblico.Web/ConhecimentoBiblico.Web.csproj
```

Opcional — verificar seed via SQL Server (localdb):
```sql
SELECT Nome, TipoElemento, ElementoCentral FROM ElementosBiblicos;
-- Esperado: 12 linhas (7 personagens + 5 temas), Jesus com ElementoCentral = 1

SELECT e1.Nome AS Origem, cb.TipoConexao, e2.Nome AS Destino
FROM ConexoesBiblicas cb
JOIN ElementosBiblicos e1 ON cb.OrigemId = e1.Id
JOIN ElementosBiblicos e2 ON cb.DestinoId = e2.Id;
-- Esperado: 6 linhas

SELECT cb.Referencia FROM ConexaoFontesBiblicas cb;
-- Esperado: 6 linhas
```

- [ ] **Step 5: Executar todos os testes para confirmar domínio intacto**

```powershell
dotnet test
```

Esperado: todos os testes passando, 0 falhas.

- [ ] **Step 6: Commit final**

```powershell
git add src/ConhecimentoBiblico.Infrastructure/Migrations/
git commit -m "feat: adicionar migration inicial com schema completo e seed de dados"
```

---

## Critério de Sucesso

- [ ] `dotnet build` — todos os 5 projetos compilam sem erros
- [ ] `dotnet test` — todos os testes passam
- [ ] `dotnet ef database update` — banco criado com 6 tabelas
- [ ] Seed: 12 elementos, 6 conexões, 6 fontes bíblicas, 2 perguntas
- [ ] Jesus é o único elemento com `ElementoCentral = true`
- [ ] Query de conexões de Jonas retorna 2 registros
