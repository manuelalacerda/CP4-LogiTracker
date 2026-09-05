# 🚚 LogiTracker - Sistema de Gestão Logística (CP4)

Este projeto faz parte da avaliação **CP4 (Check Point 4)** do curso de **Análise e Desenvolvimento de Sistemas - FIAP**.

O projeto consiste na evolução da API REST desenvolvida nos CPs anteriores, mantendo a arquitetura existente e adicionando:

* **Health Checks** para verificar a disponibilidade da aplicação e do banco de dados;
* **Observabilidade**, com logs estruturados e correlação por `traceId`;
* **Testes automatizados** utilizando xUnit e Moq;
* Evidências de funcionamento dos recursos implementados.

---

## 👥 Integrantes

* **Nome:** Manuela de Lacerda Soares
  **RM:** 564887

* **Nome:** Sofia Siqueira Fontes
  **RM:** 563829

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture**, mantendo a separação de responsabilidades entre as camadas:

```text
LogiTracker
│
├── LogiTracker.API
│   ├── Controllers
│   ├── Extensions
│   ├── Health
│   ├── Middleware
│   └── Program.cs
│
├── LogiTracker.Application
│   ├── Interfaces
│   ├── Services
│   └── DTOs
│
├── LogiTracker.Domain
│   ├── Entities
│   ├── Exceptions
│   └── Interfaces
│
├── LogiTracker.Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Migrations
│
├── LogiTracker.Domain.Tests
│
└── LogiTracker.Application.Tests
```

### Responsabilidade das camadas

* **API:** endpoints REST, composição da aplicação, Health Checks, logs e tratamento global de exceções.
* **Application:** serviços e regras de aplicação.
* **Domain:** entidades, regras de negócio e contratos do domínio.
* **Infrastructure:** persistência, Entity Framework Core, `DbContext`, migrations e repositórios.
* **Domain.Tests:** testes unitários das regras do domínio.
* **Application.Tests:** testes unitários dos serviços da aplicação utilizando mocks dos repositórios.

---

# 🎯 Domínio

O sistema **LogiTracker** atua no domínio de **Logística e Transportes**.

A aplicação permite o gerenciamento de operações relacionadas ao transporte e entrega de cargas, incluindo:

* Gerenciamento de transportadoras;
* Controle de veículos;
* Controle de motoristas;
* Cadastro e gerenciamento de cargas;
* Rastreamento e gerenciamento de entregas;
* Associação entre carga, veículo e motorista.

---

## 🧩 Entidades Modeladas

### Carrier — Transportadora

Representa a empresa responsável pelas operações de transporte.

### Vehicle — Veículo

Representa os veículos pertencentes à frota da transportadora.

### Driver — Motorista

Representa os profissionais responsáveis pela condução dos veículos.

### Cargo — Carga

Representa a carga que será transportada.

### Delivery — Entrega

Representa uma operação logística, relacionando carga, veículo e motorista.

---

# 🗄️ Banco de Dados

* **SGBD:** Oracle
* **ORM:** Entity Framework Core
* **Persistência:** Repository Pattern
* **Migrations:** Entity Framework Core Migrations

O banco de dados utilizado nesta versão é o mesmo definido nos CPs anteriores.

## 🔄 Atualização do banco

Com o Oracle configurado e a connection string definida no ambiente local, executar:

```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
```

> Não devem ser commitadas credenciais reais no repositório.

---

# ▶️ Como executar a aplicação

## Pré-requisitos

É necessário possuir instalado:

* .NET SDK compatível com o projeto;
* Oracle Database ou ambiente Oracle acessível;
* Entity Framework Core CLI, caso seja necessário executar migrations.

Para verificar a instalação do .NET:

```bash
dotnet --version
```

---

## 1. Restaurar as dependências

Na raiz da solução:

```bash
dotnet restore
```

---

## 2. Compilar a solução

```bash
dotnet build
```

---

## 3. Configurar o banco de dados

Configure a connection string do Oracle de acordo com o ambiente local.

Depois, execute as migrations:

```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
```

---

## 4. Executar a API

Na raiz do projeto:

```bash
dotnet run --project LogiTracker.API
```

A aplicação estará disponível em:

```text
http://localhost:5138
```

---

# 🔗 URLs da Aplicação

### Swagger

```text
http://localhost:5138/swagger/index.html
```

O Swagger permite visualizar e testar os endpoints da API.

### Health Check

```text
http://localhost:5138/health
```

O endpoint `/health` apresenta o estado operacional da aplicação e suas dependências.

> O Health Check não é um endpoint de negócio e não substitui os endpoints REST existentes.

---

## Endpoint

Existe somente um endpoint de Health Check:

```http
GET /health
```

O endpoint retorna o relatório completo dos checks.

Exemplo de resposta saudável:

```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "self": {
      "status": "Healthy",
      "duration": "00:00:00.0010000"
    },
    "oracle-db": {
      "status": "Healthy",
      "duration": "00:00:00.1200000"
    }
  }
}
```

Os valores de duração são apenas ilustrativos; os valores reais dependem da execução local.

---

## Status HTTP

O endpoint utiliza os seguintes códigos HTTP:

| Status do Health Check |                      HTTP |
| ---------------------- | ------------------------: |
| `Healthy`              |                  `200 OK` |
| `Degraded`             |                  `200 OK` |
| `Unhealthy`            | `503 Service Unavailable` |

Dessa forma, uma falha no banco de dados faz com que o status agregado do `/health` seja `Unhealthy`, retornando `503`.

---

# 📊 Observabilidade e Logs

A aplicação utiliza `ILogger<T>` para geração de logs estruturados.

Os logs utilizam propriedades nomeadas e o identificador de correlação da requisição:

```text
HttpContext.TraceIdentifier
```

Esse identificador é apresentado como:

```text
traceId
```

permitindo acompanhar uma requisição desde seu início até o resultado ou eventual erro.

---

## Logs no fluxo de escrita

Em pelo menos um fluxo de escrita da aplicação são registrados:

* início da operação;
* sucesso da operação;
* propriedades relevantes da operação;
* `traceId`.

Exemplo conceitual:

```text
Iniciando criação de entrega.
VehicleId: ...
DriverId: ...
CargoId: ...
TraceId: ...
```

e posteriormente:

```text
Entrega criada com sucesso.
DeliveryId: ...
TraceId: ...
```

Os logs utilizam propriedades estruturadas em vez de apenas concatenar valores em uma string.

---

## GlobalExceptionHandler

O `GlobalExceptionHandler` permanece responsável pelo tratamento centralizado das exceções da API.

Quando ocorre uma exceção não tratada, ela é registrada em nível `Error`, contendo:

* a exceção;
* mensagem;
* `traceId`.

Exemplo:

```text
Exceção não tratada: ...
TraceId: ...
```

O `traceId` permite relacionar a resposta de erro recebida pelo cliente com o evento correspondente no log.

---

# 🧪 Testes Automatizados

A solução possui dois projetos de testes:

```text
LogiTracker.Domain.Tests
LogiTracker.Application.Tests
```

Os testes utilizam:

* **xUnit**
* **Microsoft.NET.Test.Sdk**
* **Moq**
* Runner do Visual Studio

---

# 🧠 Testes de Domain

Projeto:

```text
LogiTracker.Domain.Tests
```

O projeto referencia somente a camada de Domain.

Os testes não utilizam mocks nem dependem de:

* API;
* Infrastructure;
* banco de dados.

As regras de negócio são testadas diretamente nas entidades do domínio.

---

# 🧩 Testes de Application

Projeto:

```text
LogiTracker.Application.Tests
```

Os testes verificam serviços existentes da camada Application.

As interfaces dos repositórios são substituídas por mocks utilizando **Moq**.

---

## Cenário de erro

É validado o comportamento quando uma dependência necessária não existe.

Nesse cenário:

1. O serviço identifica a ausência da dependência;
2. A exceção de domínio/aplicação correspondente é lançada;
3. O método de persistência não deve ser executado.

A chamada de persistência é verificada utilizando:

```csharp
Times.Never
```

---

# ▶️ Executando os testes

Os testes podem ser executados a partir da raiz da solução com:

```bash
dotnet test
```

Para uma execução mais detalhada:

```bash
dotnet test --verbosity normal
```

A entrega deve conter a evidência da execução com todos os testes passando.

Exemplo esperado:

```text
Test Run Successful.
Total tests: XX
     Passed: XX
     Failed: 0
     Skipped: 0
```

> Substituir `XX` pela quantidade apresentada na execução real do projeto.

---

# 🛡️ Tratamento de Exceções

O projeto mantém o `GlobalExceptionHandler` desenvolvido nos CPs anteriores.

As exceções de domínio continuam sendo convertidas para respostas HTTP utilizando `ProblemDetails`.

A tabela de mapeamento utilizada no CP3 permanece válida nesta versão.

| Exceção                     |                 HTTP Status |
| --------------------------- | --------------------------: |
| `ResourceNotFoundException` |             `404 Not Found` |
| `BusinessRuleException`     |           `400 Bad Request` |
| `ConflictException`         |              `409 Conflict` |
| Exceções não tratadas       | `500 Internal Server Error` |

> Caso os nomes exatos das exceções do CP3 sejam diferentes no código-fonte, esta tabela deve ser ajustada para reproduzir exatamente o mapeamento existente no `GlobalExceptionHandler`.

---

# 📁 Evidências

As evidências da implementação do CP4 estão organizadas na pasta:

```text
/docs/
```
Devem ser apresentadas evidências dos seguintes cenários:

## 1. Health Check — Healthy

A API e o banco devem estar disponíveis.

Resultado esperado:

```http
GET /health
```

```text
HTTP 200 OK
```

com os checks `self` e `oracle-db` identificados como `Healthy`.

---

## 2. Health Check — Unhealthy

Com o banco indisponível ou com uma connection string inválida no ambiente local:

```http
GET /health
```

Resultado esperado:

```text
HTTP 503 Service Unavailable
```

O check do banco deve aparecer como `Unhealthy`.

> Não utilizar credenciais reais ou informações sensíveis nas evidências ou no repositório.

---

# 📌 Comandos principais

### Restaurar dependências

```bash
dotnet restore
```

### Compilar

```bash
dotnet build
```

### Atualizar banco

```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
```

### Executar API

```bash
dotnet run --project LogiTracker.API
```

### Executar testes

```bash
dotnet test
```

---

# 📚 Relação com os CPs anteriores

| CP      | Entrega                                                                           |
| ------- | --------------------------------------------------------------------------------- |
| **CP1** | MER + entidades em C#                                                             |
| **CP2** | Banco de dados + Entity Framework Core + migrations                               |
| **CP3** | API REST + Swagger + Repository Pattern + ProblemDetails + GlobalExceptionHandler |
| **CP4** | Health Checks + Logs estruturados + testes automatizados                          |

O CP4 mantém a estrutura desenvolvida nos CPs anteriores e acrescenta recursos de **operabilidade, observabilidade e testes automatizados** sobre o mesmo sistema.

---

# 🎯 Objetivo do CP4

O objetivo desta etapa é garantir que a API não apenas funcione, mas também possa ser monitorada, diagnosticada e validada automaticamente.

Os três principais pilares implementados são:

```text
┌─────────────────────┐
│    HEALTH CHECKS    │
│                     │
│ API + Banco Oracle  │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   OBSERVABILIDADE   │
│                     │
│ Logs + traceId      │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   TESTES UNITÁRIOS  │
│                     │
│ Domain + Application│
└─────────────────────┘
```

A implementação segue a proposta do CP4 de manter as responsabilidades separadas dentro da arquitetura:

* **Health Checks e logs:** composição da API;
* **Regras de negócio:** Domain;
* **Regras de aplicação:** Application;
* **Persistência:** Infrastructure.
