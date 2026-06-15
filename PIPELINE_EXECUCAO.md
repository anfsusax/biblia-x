PIPELINE_EXECUCAO# PIPELINE DE EXECUÇÃO - CONHECIMENTO BÍBLICO

## 🎯 Objetivo do Pipeline

Controlar a evolução do conhecimento bíblico como um sistema estruturado, garantindo:

- Ordem correta de evolução do domínio
- Evitar criação prematura de software
- Evitar expansão descontrolada de conteúdo
- Separação clara de responsabilidades entre agentes

---

# 🧭 ESTADO ATUAL DO SISTEMA

## ESTADO ATIVO

- DOMINIO

## ESTADOS POSSÍVEIS

### 1. DOMINIO

Foco:

- Compreensão dos conceitos bíblicos fundamentais
- Definição de entidades conceituais
- Relações naturais entre elementos

Proibido:

- Narrativas estruturadas (capítulos)
- Software
- APIs
- Modelagem técnica

---

### 2. MODELAGEM

Foco:

- Organização do conhecimento em estruturas consistentes
- Definição de domínios e relacionamentos
- Consolidação de vocabulário

Proibido:

- Implementação de jornada
- Software
- Código

---

### 3. JORNADA

Foco:

- Estruturação cronológica da vida de Jesus
- Organização dos capítulos
- Conexão entre eventos

Permitido:

- Capítulos
- Sprints
- DoD de conhecimento

Proibido:

- Expansão de novos domínios

---

### 4. CONEXAO

Foco:

- Conectar personagens, eventos, profecias e ensinamentos
- Criar links entre domínios existentes

Proibido:

- Criar novos capítulos da jornada

---

### 5. SOFTWARE

Foco:

- Apenas quando o conhecimento estiver maduro
- Implementação técnica (C#, APIs, etc.)

---

# 🚦 REGRA DE CONTROLE ABSOLUTO

## Nenhum agente pode alterar o estado

Somente o ORQUESTRADOR (usuário) pode mudar o estado.

---

# 🧠 PAPEL DOS AGENTES

## ChatGPT (Arquitetura e Controle de Domínio)

Responsável por:

- Validar se algo pertence ao domínio
- Detectar overengineering
- Impedir expansão prematura
- Definir se algo pode ou não avançar de fase

---

## Claude (Executor de Conhecimento)

Responsável por:

- Gerar documentos .md
- Estruturar conhecimento conforme estado atual
- NÃO decidir arquitetura
- NÃO avançar estados
- Seguir estritamente o PIPELINE_EXECUCAO.md

Regra crítica:
> Claude não cria conhecimento novo fora do estado atual

---

## Gemini (Validação Externa)

Responsável por:

- Validar coerência histórica e bíblica
- Identificar inconsistências factuais
- Corrigir interpretações erradas de contexto

---

## Cursor (Implementação)

Responsável por:

- Só atua quando estado = SOFTWARE
- Implementação técnica real
- Código, APIs, arquitetura

---

# 🧱 REGRAS DE TRANSIÇÃO DE ESTADO

Mudança de estado só ocorre quando:

- Domínio está suficientemente compreendido
- Não há ambiguidades críticas
- Não há expansão prematura

Critério:

> “O conhecimento atual permite avanço sem perda de coerência?”

Se não → permanece no estado atual.

---

# ⚠️ REGRAS DE PROTEÇÃO

É proibido:

- Pular estados
- Criar narrativa antes do domínio
- Criar software antes da modelagem
- Criar entidades não justificadas
- Expandir o sistema por curiosidade técnica

---

# 🧭 FLUXO CORRETO

DOMINIO
↓
MODELAGEM
↓
JORNADA
↓
CONEXAO
↓
SOFTWARE

---

# 🔒 PRINCÍPIO FUNDAMENTAL

> Nenhuma camada superior pode ser construída sem estabilidade da camada inferior.

---

# 📌 REGRA FINAL

Se houver dúvida:

❌ não avance
✔ simplifique
✔ refine domínio
✔ valide entendimento
