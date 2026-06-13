# Design: ConhecimentoBiblico — Modelo Final de Domínio

**Data:** 2026-06-12
**Versão:** 3.0 — Modelo Final Aprovado
**Status:** Aprovado para implementação
**Escopo:** MVP — Knowledge Graph bíblico cristocêntrico com camada de ensino

---

## Visão do Produto

Plataforma de **ensino** bíblico cristocêntrico. O objetivo não é armazenar registros bíblicos, mas representar um **Knowledge Graph** onde qualquer elemento bíblico pode se relacionar com qualquer outro — e tudo aponta para Jesus — traduzido em perguntas respondidas pelo próprio grafo.

**Perguntas que o sistema responde:**
- O que é casamento? O que é fé? O que é perdão?
- Por que Jesus citou Jonas?
- O que Paulo ensina sobre salvação?
- Quais passagens apontam para Cristo?

**Princípio central:** o conhecimento vive no grafo. A resposta é construída pela camada Application percorrendo as conexões. O domínio não armazena respostas estáticas.

---

## Arquitetura

```
src/
  ConhecimentoBiblico.Domain/
  ConhecimentoBiblico.Application/
  ConhecimentoBiblico.Infrastructure/
  ConhecimentoBiblico.Web/
tests/
  ConhecimentoBiblico.UnitTests/
```

| Projeto | Responsabilidade | Dependências |
|---|---|---|
| `Domain` | Entidades, enums, interfaces de repositório | Nenhuma |
| `Application` | Use cases futuros, interfaces de serviço | Domain |
| `Infrastructure` | EF Core, SQL Server, mapeamentos, seed | Domain, Application |
| `Web` | Blazor Web App, Razor Components | Application |
| `UnitTests` | Testes de Domain e Application em isolamento | Domain, Application |

**Stack:** ASP.NET Core 9 · Blazor Web App · Entity Framework Core · SQL Server · Clean Architecture

---

## Estrutura do Domain

O Domain é dividido em dois namespaces com dependência unidirecional: `Ensino` referencia `Conhecimento`, nunca o contrário.

```
Domain/
  Conhecimento/     ← Layer 1: o grafo de conhecimento
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
    FonteBiblica.cs       ← value object
    Enums/
      TipoElementoBiblico.cs
      TipoConexao.cs
      Testamento.cs

  Ensino/           ← Layer 2: perguntas, reflexões e trilhas
    PerguntaBiblica.cs
    ReflexaoBiblica.cs
    (TrilhaDeEstudo — fase futura)
```

---

## Layer 1 — Grafo de Conhecimento

### ElementoBiblico (abstract) — TPH

Classe abstrata raiz de todos os nós do grafo. Tabela única `ElementosBiblicos` via TPH com discriminador `TipoElemento`.

| Propriedade | Tipo | Observação |
|---|---|---|
| `Id` | Guid | |
| `Nome` | string | |
| `Descricao` | string | |
| `TipoElemento` | TipoElementoBiblico | Discriminador EF Core |
| `ElementoCentral` | bool | Marca nós de referência cristocêntrica. Jesus = true. |

**Regras:** construtor `protected`, factory method `Criar(...)` em cada subclasse, sem setters públicos.

### Subclasses Concretas

| Classe | TipoElemento | Propriedades Adicionais |
|---|---|---|
| `PersonagemBiblico` | `Personagem` | `PeriodoHistorico` (string), `Ocupacao` (string) |
| `TemaBiblico` | `Tema` | — |
| `EventoBiblico` | `Evento` | — |
| `PassagemBiblica` | `Passagem` | `Livro` (string), `Capitulo` (int), `VersiculoInicial` (int), `VersiculoFinal` (int?), `TextoResumo` (string) |
| `ProfeciaBiblica` | `Profecia` | `Cumprida` (bool), `TextoCumprimento` (string?) |
| `ParabolaBiblica` | `Parabola` | `LicaoCentral` (string) |
| `LivroBiblico` | `Livro` | `Testamento` (enum Testamento), `NumeroCaps` (int) |
| `LocalBiblico` | `Local` | `Regiao` (string) |

### ConexaoBiblica — Aresta do Grafo

Representa a relação direcional entre dois `ElementoBiblico`. Tabela `ConexoesBiblicas`.

| Propriedade | Tipo | Observação |
|---|---|---|
| `Id` | Guid | |
| `OrigemId` | Guid | FK → `ElementoBiblico` · `DeleteBehavior.Restrict` |
| `DestinoId` | Guid | FK → `ElementoBiblico` · `DeleteBehavior.Restrict` |
| `Origem` | nav `ElementoBiblico` | |
| `Destino` | nav `ElementoBiblico` | |
| `TipoConexao` | TipoConexao | |
| `Explicacao` | string | obrigatória |
| `FontesBiblicas` | `IReadOnlyCollection<FonteBiblica>` | OwnsMany |

**Invariantes:**
- `OrigemId ≠ DestinoId`
- `Explicacao` não pode ser vazia
- Factory: `Criar(origemId, destinoId, tipoConexao, explicacao)`
- Método: `AdicionarFonte(referencia, textoVersiculo?)`

### FonteBiblica (Value Object)

Mapeado com `OwnsMany` → tabela `ConexaoFontesBiblicas`. Igualdade por `Referencia`.

| Propriedade | Tipo |
|---|---|
| `Referencia` | string — ex: "Mateus 12:40" |
| `TextoVersiculo` | string? |

---

## Layer 2 — Ensino

### PerguntaBiblica — Aggregate Root

Representa uma pergunta do usuário final. Não armazena resposta — aponta para os nós do grafo que a respondem. A resposta é montada pela camada `Application`.

Tabela `PerguntasBiblicas`.

| Propriedade | Tipo | Observação |
|---|---|---|
| `Id` | Guid | |
| `Titulo` | string | "Casamento Cristão" — label de navegação |
| `Pergunta` | string | "O que é casamento?" — pergunta exibida ao usuário |
| `ElementosRelacionados` | `IReadOnlyCollection<ElementoBiblico>` | M:N via `PerguntaElementos` |
| `Reflexoes` | `IReadOnlyCollection<ReflexaoBiblica>` | 1:N owned |

**Invariante:** `Pergunta` não pode ser vazia.

### ReflexaoBiblica — Owned by PerguntaBiblica

Não existe fora do contexto de uma pergunta. Tabela `ReflexoesBiblicas` com FK `PerguntaBiblicaId`.

| Propriedade | Tipo |
|---|---|
| `Id` | Guid |
| `PerguntaBiblicaId` | Guid (FK obrigatória) |
| `Titulo` | string |
| `Explicacao` | string |
| `AplicacaoPratica` | string |
| `PerguntaReflexao` | string |

---

## Enums de Domínio

### TipoElementoBiblico
```
Personagem | Tema | Evento | Passagem | Profecia | Parabola | Livro | Local
```

### TipoConexao

```
// Tipologias cristocêntricas
ApontaParaCristo        // aponta genericamente para Cristo
PrefiguraCristo         // é tipo/prefigura Cristo (Jonas, cordeiro pascal)
SimbolizaCristo         // simboliza aspecto de Cristo
CumpridoPorCristo       // promessa/profecia cumprida por Cristo

// Ensino direto de Jesus (tipos dedicados — Jesus é semanticamente central)
EnsinadoPorJesus        // Jesus ensinou diretamente sobre este elemento
CitadoPorJesus          // Jesus citou este elemento

// Explicações apostólicas — genérico, OCP-compliant
// Destino da conexão define o autor (Paulo, Pedro, João, Tiago, Lucas...)
ExplicadoPor

// Relações proféticas
Profecia
CumprimentoDeProfecia

// Relações contextuais
ContextoHistorico
PassagemParalela
RelacionadoAoTema
RelacionadoAoPersonagem
RelacionadoAoEvento
RelacionadoAoLocal
```

**Decisão sobre ExplicadoPor:** `ExplicadoPorPaulo/Pedro/João` foram substituídos por `ExplicadoPor` genérico. O autor está no `DestinoId` da conexão. Query: `TipoConexao = ExplicadoPor AND DestinoId = {idPaulo}`. Adicionar Tiago, Lucas, Judas não requer alteração do enum — apenas seed.

### Testamento
```
Antigo | Novo
```

---

## Diagrama Completo

```
ConhecimentoBiblico.Domain/
│
├── Conhecimento/
│   │
│   │  ElementoBiblico (abstract · TPH → ElementosBiblicos)
│   │  ├── Id, Nome, Descricao, TipoElemento, ElementoCentral
│   │  │
│   │  ├── PersonagemBiblico   + PeriodoHistorico, Ocupacao
│   │  ├── TemaBiblico
│   │  ├── EventoBiblico
│   │  ├── PassagemBiblica     + Livro, Cap, VersIni, VersFin?, TextoResumo
│   │  ├── ProfeciaBiblica     + Cumprida, TextoCumprimento?
│   │  ├── ParabolaBiblica     + LicaoCentral
│   │  ├── LivroBiblico        + Testamento, NumeroCaps
│   │  └── LocalBiblico        + Regiao
│   │
│   │  ConexaoBiblica (→ ConexoesBiblicas)
│   │  ├── OrigemId  ──→ ElementoBiblico [Restrict]
│   │  ├── DestinoId ──→ ElementoBiblico [Restrict]
│   │  ├── TipoConexao : TipoConexao
│   │  ├── Explicacao : string
│   │  └── FontesBiblicas [OwnsMany → ConexaoFontesBiblicas]
│   │       └── FonteBiblica: Referencia, TextoVersiculo?
│   │
│   └── Enums/
│       ├── TipoElementoBiblico
│       ├── TipoConexao
│       └── Testamento
│
└── Ensino/

    │  PerguntaBiblica (→ PerguntasBiblicas)
    │  ├── Titulo, Pergunta
    │  ├── ElementosRelacionados [M:N → PerguntaElementos]
    │  └── Reflexoes [1:N → ReflexoesBiblicas]
    │
    │  ReflexaoBiblica (owned)
    │  ├── PerguntaBiblicaId : Guid
    │  ├── Titulo, Explicacao
    │  ├── AplicacaoPratica
    │  └── PerguntaReflexao
    │
    └── (TrilhaDeEstudo — fase futura)
```

---

## Mapeamento EF Core

### Tabelas

| Tabela | Origem |
|---|---|
| `ElementosBiblicos` | TPH — todas as subclasses |
| `ConexoesBiblicas` | `ConexaoBiblica` |
| `ConexaoFontesBiblicas` | `OwnsMany<FonteBiblica>` |
| `PerguntasBiblicas` | `PerguntaBiblica` |
| `PerguntaElementos` | join M:N |
| `ReflexoesBiblicas` | `ReflexaoBiblica` com FK `PerguntaBiblicaId` |

### Configurações Críticas

```csharp
// TPH com discriminador tipado
builder.HasDiscriminator<TipoElementoBiblico>(e => e.TipoElemento)
    .HasValue<PersonagemBiblico>(TipoElementoBiblico.Personagem)
    // ...

// ConexaoBiblica — sem cascade em ciclos de grafo
builder.HasOne(c => c.Origem).WithMany()
    .HasForeignKey(c => c.OrigemId).OnDelete(DeleteBehavior.Restrict);
builder.HasOne(c => c.Destino).WithMany()
    .HasForeignKey(c => c.DestinoId).OnDelete(DeleteBehavior.Restrict);

// FonteBiblica — value object como owned collection
builder.OwnsMany(c => c.FontesBiblicas, fb => {
    fb.ToTable("ConexaoFontesBiblicas");
    fb.Property(f => f.Referencia).IsRequired().HasMaxLength(50);
});

// PerguntaBiblica — many-to-many com ElementoBiblico
builder.HasMany(p => p.ElementosRelacionados)
    .WithMany()
    .UsingEntity("PerguntaElementos");
```

---

## Seed Inicial

### PersonagemBiblico

| Nome | PeriodoHistorico | Ocupacao | ElementoCentral |
|---|---|---|---|
| Jesus | Século I d.C. | Filho de Deus, Messias | **true** |
| Paulo | Século I d.C. | Apóstolo, Teólogo | false |
| Pedro | Século I d.C. | Apóstolo, Pescador | false |
| João | Século I d.C. | Apóstolo, Evangelista | false |
| Jonas | Século VIII a.C. | Profeta | false |
| Moisés | Século XIII a.C. | Profeta, Legislador | false |
| Davi | Século X a.C. | Rei, Salmista | false |

### TemaBiblico

Fé · Casamento · Perdão · Esperança · Salvação (todos `ElementoCentral = false`)

### ConexoesBiblicas

| Origem | TipoConexao | Destino | Fonte |
|---|---|---|---|
| Jonas | PrefiguraCristo | Jesus | Mateus 12:40 |
| Moisés | PrefiguraCristo | Jesus | João 5:46 |
| Davi | ApontaParaCristo | Jesus | Lucas 20:41-44 |
| Jonas | CitadoPorJesus | Jesus | Mateus 12:39-40 |
| Casamento | ExplicadoPor | Paulo | Efésios 5:25-32 |
| Perdão | ExplicadoPor | Paulo | Efésios 4:32 |

### PerguntaBiblica (seed)

| Titulo | Pergunta | ElementosRelacionados |
|---|---|---|
| Jonas e a Ressurreição | Por que Jesus citou Jonas? | Jonas, Jesus |
| O Que é Casamento? | O que a Bíblia ensina sobre casamento? | Casamento, Paulo |

---

## O Que Este Design NÃO Inclui (intencionalmente)

- `RespostaCurta` / `RespostaDetalhada` — conhecimento vive no grafo, resposta é montada pelo Application
- CRUD / use cases / camada de serviço
- Autenticação e autorização
- Telas Blazor funcionais
- CQRS / MediatR / eventos de domínio
- Value Objects além de `FonteBiblica`
- `TrilhaDeEstudo` — namespace preparado, implementação futura
- IA / geração de conteúdo

---

## Plano de Evolução Futura

| Fase | Evolução |
|---|---|
| 2 | Interfaces de repositório no Domain |
| 2 | Value Objects: `ReferenciaPassagem`, `PeriodoHistorico` |
| 3 | CQRS: queries de grafo (`ObterConexoesDe`, `ObterCaminhoAteJesus`) |
| 4 | `TrilhaDeEstudo` no namespace `Ensino/` |
| 4 | Eventos de domínio |
| 5 | `PersonagemBiblico.Genealogia` — self-referencing |
| 6 | Graph traversal: caminho mais curto de X até Jesus |

---

## Critério de Sucesso do MVP

1. Solução compila sem erros
2. `dotnet ef migrations add Inicial` gera migration válida com as 6 tabelas
3. Seed popula 12 `ElementosBiblicos` e 6 `ConexoesBiblicas` com fontes
4. Query EF "conexões de Jonas" retorna 2 registros com `FontesBiblicas` populadas
5. Query EF "perguntas ligadas ao tema Casamento" retorna 1 registro com reflexões
