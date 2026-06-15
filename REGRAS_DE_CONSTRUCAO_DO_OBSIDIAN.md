# REGRAS_DE_CONSTRUCAO_DO_OBSIDIAN.md

---

## Objetivo

Garantir que a base de conhecimento cresça de forma sustentável, navegável e centrada em Cristo.

Estas regras valem para toda nota criada no Obsidian — independente do domínio ou fase do pipeline.

---

## Regra 1 — Uma Nota, Um Elemento

Cada nota representa exatamente um elemento de um domínio.

Correto:
- `Pessoas/Jesus.md`
- `Eventos/Batismo-de-Jesus.md`
- `Conceitos/Reino-de-Deus.md`

Errado:
- `Jesus-e-seu-batismo.md` (mistura Pessoa e Evento)
- `Eventos-do-ministerio.md` (plural — deve ser desmembrado)

---

## Regra 2 — Fonte Única de Verdade

Cada informação tem um único lugar canônico.

Toda outra nota que precisar dessa informação usa um link — nunca copia o conteúdo.

Exemplo: a biografia de Maria vive em `Pessoas/Maria.md`. Qualquer outra nota que mencionar Maria usa `[[Maria]]`.

Nunca duplicar descrições. Nunca duplicar biografias.

---

## Regra 3 — Estrutura Padrão de Nota

Toda nota segue esta estrutura, adaptada ao seu domínio:

```
# [Nome do Elemento]

**Domínio:** Pessoa | Evento | Lugar | Conceito | Ensino | Profecia
**Tipo:** [especialização, se houver]
**Referências Bíblicas:** [passagens realmente lidas]

## O que é
[Definição objetiva — o que o texto diz, não interpretação]

## Contexto
[Momento histórico, cultural ou narrativo]

## Relacionamentos
[Links para outros elementos usando [[WikiLink]]]

## Dependências
[O que precisou existir ou acontecer antes]

## Consequências
[O que ficou possível ou inevitável depois]

## Princípios Observados
[Padrões observáveis no texto — não forçados]

## Aplicação Pessoal
[Como este elemento fala para a vida cotidiana]

## Aplicação Arquitetural
[Princípio de missão, planejamento, liderança ou execução extraível]

## Como aponta para Cristo
[Resposta obrigatória. Se não conseguir responder, a nota está incompleta.]
```

---

## Regra 4 — Links Explícitos

Sempre usar links para conectar conhecimento.

Formato: `[[Nome-do-Elemento]]`

Exemplo:
> Jesus foi batizado por [[João Batista]] durante o [[Batismo de Jesus]].

Nunca mencionar um elemento de outro domínio sem linkar.

---

## Regra 5 — Tags de Domínio

Toda nota deve ter exatamente uma tag de domínio:

```
#pessoa
#evento
#lugar
#conceito
#ensino
#profecia
```

Tags opcionais de especialização:

```
#evento/milagre
#evento/julgamento
#ensino/parabola
#ensino/sermao
```

---

## Regra 6 — Crescimento Incremental

Criar apenas aquilo que já foi estudado.

Não criar notas futuras por antecipação.

Não criar estruturas hipotéticas.

Se o elemento ainda não foi estudado, ele não existe na base.

---

## Regra 7 — Controle por Estado do Pipeline

A criação de notas é restrita pelo estado atual do pipeline:

| Estado     | O que pode ser criado                         |
|------------|-----------------------------------------------|
| DOMINIO    | Definições conceituais dos domínios           |
| MODELAGEM  | Estruturas de relacionamento entre domínios   |
| JORNADA    | Notas de Eventos, Pessoas e Lugares da jornada|
| CONEXAO    | Links entre elementos existentes              |
| SOFTWARE   | Nada — o Obsidian não é tocado nesta fase     |

Criar uma nota fora do estado atual é uma violação do pipeline.

---

## Regra 8 — Proibição de Enciclopédia

Não registrar informações apenas por curiosidade histórica.

Cada nota deve contribuir para a compreensão da narrativa bíblica e de sua relação com Cristo.

Sinal de alerta: se uma nota não consegue preencher "Como aponta para Cristo", ela provavelmente não deveria existir ainda.

---

## Regra 9 — Relacionamentos Bidirecionais

Se A aponta para B, verificar se B deveria apontar de volta para A.

Exemplo:
- `[[Batismo de Jesus]]` na nota de João Batista
- `[[João Batista]]` na nota do Batismo de Jesus

O grafo só é navegável se os links forem bidirecionais onde faz sentido.

---

## Regra 10 — Revisão de Nota

Antes de considerar uma nota concluída, verificar:

- [ ] A nota representa um único elemento?
- [ ] Não há informação duplicada de outra nota?
- [ ] Todos os elementos mencionados estão linkados?
- [ ] A tag de domínio está correta?
- [ ] "Como aponta para Cristo" foi respondido?
- [ ] Há referência bíblica verificável?

Se qualquer resposta for não → a nota não está concluída.

---

## Regra 11 — Ordem de Criação de Notas

A base cresce nesta sequência — nunca pulando etapas:

1. Pessoas da Jornada de Jesus
2. Eventos da Jornada de Jesus
3. Lugares relevantes
4. Conceitos revelados pelos eventos
5. Ensinamentos associados
6. Profecias cumpridas

Após maturidade da Fase 1 de Jesus, o ciclo reinicia para Paulo, Pedro, e assim por diante.

---

## Princípio Final

A base de conhecimento é um grafo centrado em Cristo.

Cada nota é um nó.

Cada link é uma aresta.

O grafo só tem valor se todo caminho puder ser traçado de volta à missão de Jesus.
