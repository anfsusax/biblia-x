# Domínio Bíblico — Mapeamento Conceitual

**Estado do pipeline:** DOMINIO
**Proibido neste documento:** modelagem técnica, código, capítulos de jornada, entidades de banco de dados

---

## Princípio Orientador

Todo conceito identificado deve responder:

> "Este conceito existe no texto bíblico ou estou projetando uma categoria externa sobre ele?"

Se a resposta for "estou projetando" — o conceito não pertence a este mapeamento.

---

## Domínios Fundamentais

Os domínios fundamentais são os tipos de "coisas" que existem no texto bíblico e que não podem ser reduzidos a outros.

---

### 1. Pessoa

**Definição conceitual:**
Um ser humano (ou ser com identidade própria) que age, decide, falha, cresce ou declina dentro da narrativa bíblica.

**Critério de inclusão:**
Tem nome próprio ou papel identificável. Age com intenção — mesmo que limitada.

**Exemplos confirmados:**
- Jesus
- Maria
- José
- João Batista
- Pedro
- Pilatos

**Nota:**
Deus e o Espírito Santo são presenças na narrativa. Não serão modelados como "Pessoa" neste sistema — sua natureza transcende a categoria. Quando aparecerem, serão registrados como contexto ou referência, não como elemento do grafo.

---

### 2. Evento

**Definição conceitual:**
Uma ocorrência situada no tempo e no espaço que muda o estado da narrativa. Algo aconteceu. O antes e o depois são diferentes.

**Critério de inclusão:**
Tem consequência identificável. Pode ser datado (mesmo que aproximadamente). Altera o curso de algo.

**Exemplos confirmados:**
- Nascimento de Jesus
- Batismo no Jordão
- Tentação no deserto
- Crucificação
- Ressurreição

**Distinção importante:**
Evento ≠ Ensinamento. Jesus curando um cego é um Evento. Jesus explicando quem é o próximo (Parábola do Bom Samaritano) é um Ensinamento que pode estar associado a um Evento, mas não é o Evento em si.

---

### 3. Lugar

**Definição conceitual:**
Uma localização geográfica ou simbólica onde eventos ocorrem ou onde personagens habitam.

**Critério de inclusão:**
É mencionado como cenário relevante para a narrativa. Sua identidade importa — o mesmo evento em outro lugar teria significado diferente.

**Exemplos confirmados:**
- Belém (Nascimento)
- Nazaré (Infância)
- Rio Jordão (Batismo)
- Deserto da Judeia (Tentação)
- Jerusalém (Ministério, Crucificação, Ressurreição)
- Galiléia (Início do Ministério)

**Nota:**
Lugares não serão mapeados como destinos turísticos. Serão mapeados quando o lugar for relevante para o significado do evento.

---

### 4. Conceito

**Definição conceitual:**
Uma ideia, tema ou verdade teológica que atravessa a narrativa sem ser redutível a um único evento ou personagem.

**Critério de inclusão:**
Aparece em múltiplos contextos. É mais amplo que um evento. Não é a história em si — é o que a história revela.

**Exemplos confirmados:**
- Reino de Deus
- Perdão
- Fé
- Redenção
- Graça
- Aliança

**Distinção importante:**
Conceito ≠ Tema de estudo pessoal. "O que devo fazer na minha vida" não é um Conceito bíblico — é uma aplicação. O Conceito existe no texto independente do leitor.

---

### 5. Ensinamento

**Definição conceitual:**
Uma instrução, revelação ou verdade comunicada intencionalmente por um personagem — especialmente Jesus — com objetivo de transformar a compreensão ou o comportamento do ouvinte.

**Critério de inclusão:**
Foi comunicado com intenção didática. Tem destinatário identificável. Contém uma afirmação sobre como as coisas são ou como devem ser.

**Formas identificadas:**

| Forma       | Descrição                                                    |
|-------------|--------------------------------------------------------------|
| Parábola    | Narrativa fictícia com verdade embutida                      |
| Sermão      | Discurso direto com múltiplos pontos                         |
| Discurso    | Ensino situacional em resposta a uma pergunta ou confronto   |
| Declaração  | Afirmação direta ("Eu sou o caminho...")                     |

**Nota sobre Parábola:**
Parábola não é um domínio separado — é uma forma de Ensinamento. Ela foi identificada como ambiguidade e aqui está resolvida.

---

### 6. Profecia

**Definição conceitual:**
Uma declaração antecipada de algo que ainda não ocorreu, registrada no texto bíblico, e que se cumpre (ou não) em outro ponto da narrativa.

**Critério de inclusão:**
Foi registrada como profecia no próprio texto. Possui correspondência identificável no cumprimento (ou permanece sem cumprimento registrado).

**Exemplos confirmados:**
- Nascimento em Belém (Miquéias 5:2 → Mateus 2:1)
- Entrada em Jerusalém sobre um jumento (Zacarias 9:9 → Mateus 21:5)
- Traição por 30 moedas de prata (Zacarias 11:12 → Mateus 26:15)
- Ressurreição no terceiro dia (Salmo 16:10 → Atos 2:31)

**Relação com outros domínios:**
Uma Profecia sempre se conecta a um Evento (cumprimento) e frequentemente a um Personagem (quem profetizou / de quem se falava). Esta é uma das conexões mais importantes do sistema — é o que faz Antigo e Novo Testamento se comunicarem.

---

## Ambiguidades Resolvidas

### Milagre

**Status:** Resolvido.

Milagre é um **tipo de Evento** — especificamente um Evento sobrenatural com intenção reveladora. Não é um domínio separado.

Por que não é domínio:
- Um milagre sempre ocorre em algum lugar (Lugar)
- Sempre envolve alguém (Pessoa)
- Sempre tem consequência (Evento)
- Frequentemente revela um Conceito (ex: fé, poder de Deus)

Tratamento no sistema: Evento com atributo `tipo = Milagre`.

---

### Parábola

**Status:** Resolvido.

Parábola é uma **forma de Ensinamento** — especificamente um Ensinamento em forma narrativa fictícia. Não é um domínio separado.

Por que não é domínio:
- Toda Parábola é comunicada por uma Pessoa (geralmente Jesus)
- Toda Parábola revela um Conceito
- Toda Parábola tem destinatário (Pessoa ou grupo)
- A narrativa da Parábola não é um Evento histórico — é uma ficção didática

Tratamento no sistema: Ensinamento com forma `Parábola`.

---

### Carta

**Status:** Fora de escopo atual.

Carta aparecerá com relevância na jornada de Paulo. Não pertence à Fase 1.

Reavaliação: ao iniciar Fase 2 (Paulo).

---

## Relações Naturais entre Domínios

Estas relações emergem do próprio texto — não foram projetadas tecnicamente.

```
Pessoa ──── participa em ────► Evento
Evento ──── ocorre em ────────► Lugar
Evento ──── revela ───────────► Conceito
Profecia ── cumpre-se em ─────► Evento
Profecia ── fala sobre ───────► Pessoa
Ensinamento ─ comunicado por ─► Pessoa
Ensinamento ─ revela ─────────► Conceito
Ensinamento ─ associado a ────► Evento (opcional — nem todo Ensinamento tem Evento associado)
```

**Observação crítica:**
A relação mais densa do sistema é entre Profecia e Evento. É onde o Antigo e o Novo Testamento se encontram. Qualquer estudo de Jesus que ignore esta relação está incompleto.

---

## Domínios Identificados mas Não Confirmados

Estes conceitos apareceram durante a análise mas precisam de mais estudo antes de serem confirmados como domínios:

| Candidato   | Hipótese atual                          | Questão em aberto                         |
|-------------|------------------------------------------|-------------------------------------------|
| Símbolo     | Pode ser tipo de Ensinamento ou Conceito | Um símbolo é um objeto, um ato ou uma ideia? |
| Ritual      | Pode ser tipo de Evento                 | Batismo, Páscoa — são Eventos ou Rituais distintos? |
| Instituição | Igreja, Sinagoga, Templo                | Lugar, Conceito ou categoria própria?     |

**Regra:** estes candidatos permanecem como questões abertas. Não serão modelados até que o estudo os revele como necessários.

---

## Fronteira do Domínio

O que está dentro:
- Elementos identificados no texto bíblico estudado
- Relações que emergem diretamente do texto
- Conceitos com referência bíblica verificável

O que está fora:
- Interpretações teológicas não baseadas no texto direto
- Conceitos importados de outras tradições
- Categorias úteis para o software mas não presentes no domínio

---

## Critério de Conclusão do Estado DOMINIO

O estado DOMINIO está concluído quando:

1. Os 6 domínios fundamentais estão definidos sem ambiguidade
2. As relações naturais entre eles estão mapeadas
3. As ambiguidades identificadas estão resolvidas ou registradas como questões abertas
4. A fronteira do domínio está clara

**Estado atual:** 1 e 2 concluídos. 3 concluído (Milagre e Parábola resolvidos; Carta adiada). 4 definida.

**Pendência antes de avançar:**
Os três candidatos na seção "Domínios Não Confirmados" precisam de uma decisão consciente do orquestrador antes de avançar para MODELAGEM.
