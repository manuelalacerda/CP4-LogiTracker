# 🚚 LogiTracker — CP4

Projeto desenvolvido para o **Check Point 4 (CP4)** da FIAP, evoluindo a API REST desenvolvida nos checkpoints anteriores.

## 👥 Integrantes

* **Manuela de Lacerda Soares** — RM 564887
* **Sofia Siqueira Fontes** — RM 563829

## 🏗️ Domínio e Banco de Dados

**Domínio:** Logística e Transportes

**SGBD:** Oracle

A aplicação utiliza **Entity Framework Core**, Repository Pattern e migrations para persistência dos dados.

---

## ▶️ Como executar

### Pré-requisitos

* .NET SDK compatível com o projeto
* Oracle Database ou ambiente Oracle acessível
* Entity Framework Core CLI

### Restaurar dependências

```bash
dotnet restore
```

### Compilar

```bash
dotnet build
```

### Atualizar o banco

Com a connection string configurada no ambiente local:

```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
```

Não devem ser utilizadas ou commitadas credenciais reais no repositório.

### Executar a API

```bash
dotnet run --project LogiTracker.API
```

---

## 🔗 Swagger

Após iniciar a aplicação:

```text
http://localhost:5138/swagger/index.html
```

O Swagger permite visualizar e testar os endpoints da API.

---

# ❤️ Health Check

O projeto possui um único endpoint de Health Check:

```http
GET /health
```

O endpoint retorna o relatório completo dos checks.

São verificados:

* `self` — disponibilidade da aplicação;
* `oracle-db` — disponibilidade do banco Oracle.

### Exemplo de resposta

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

Os valores de duração são apenas ilustrativos.

### Status HTTP

| Status    | HTTP                    |
| --------- | ----------------------- |
| Healthy   | 200 OK                  |
| Degraded  | 200 OK                  |
| Unhealthy | 503 Service Unavailable |

Quando o banco de dados está indisponível, o `/health` deve retornar `503 Service Unavailable`.

---

# 📊 Logs e Observabilidade

A aplicação utiliza `ILogger<T>` para geração de logs estruturados.

Os logs possuem propriedades nomeadas e utilizam:

```csharp
HttpContext.TraceIdentifier
```

O identificador é apresentado como:

```text
traceId
```

Em um fluxo de escrita da aplicação são registrados:

* início da operação;
* sucesso da operação;
* propriedades relevantes da operação;
* `traceId`.

O `GlobalExceptionHandler` registra exceções não tratadas em nível `Error`, contendo a exceção, a mensagem e o `traceId`.

Em ambiente de produção, a resposta de erro não deve expor stack trace.

---

# 🧪 Testes Automatizados

A solução possui dois projetos de testes:

```text
LogiTracker.Domain.Tests
LogiTracker.Application.Tests
```

Os testes utilizam:

* xUnit;
* `Microsoft.NET.Test.Sdk`;
* Moq.

## Domain.Tests

O projeto `LogiTracker.Domain.Tests` referencia somente o projeto **Domain**.

Os testes:

* não utilizam mocks;
* não dependem da API;
* não dependem da Infrastructure;
* não dependem de banco de dados;
* testam regras reais de negócio do domínio.

São utilizados:

```csharp
[Fact]
```

para cenários de sucesso e:

```csharp
[Theory]
[InlineData(...)]
```

para cenários de erro.

Os testes seguem o padrão **Arrange, Act, Assert (AAA)**.

## Application.Tests

O projeto `LogiTracker.Application.Tests` testa serviços existentes da camada Application.

As interfaces dos repositórios são substituídas por mocks utilizando Moq.

No cenário de erro, quando uma dependência necessária não existe:

* a exceção correspondente é lançada;
* a operação de persistência não é executada;
* a chamada é verificada com:

```csharp
Times.Never
```

No caminho feliz, a operação esperada é verificada com:

```csharp
Times.Once
```

### Executar os testes

```bash
dotnet test
```

A entrega deve apresentar evidência da execução com todos os testes passando.

---

# 🛡️ Tratamento de Exceções

O projeto mantém o `GlobalExceptionHandler` desenvolvido nos CPs anteriores.

As exceções são convertidas para respostas HTTP utilizando `ProblemDetails`.

| Exceção                     | HTTP                      |
| --------------------------- | ------------------------- |
| `ResourceNotFoundException` | 404 Not Found             |
| `DomainException`           | 400 Bad Request           |
| `ArgumentException`         | 400 Bad Request           |
| `KeyNotFoundException`      | 404 Not Found             |
| `InvalidOperationException` | 409 Conflict              |
| Outras exceções             | 500 Internal Server Error |

---

# 📁 Evidências

As evidências do CP4 estão organizadas na pasta:

```text
/docs/
```

Devem ser apresentadas evidências de:

1. **Health Check Healthy**

   * `GET /health`
   * HTTP `200 OK`
   * `self` e `oracle-db` como `Healthy`.

2. **Health Check Unhealthy**

   * banco indisponível ou connection string inválida;
   * `GET /health`
   * HTTP `503 Service Unavailable`;
   * `oracle-db` como `Unhealthy`.

3. **Logs**

   * fluxo de escrita com início e sucesso;
   * propriedades estruturadas;
   * `traceId`;
   * evidência do `GlobalExceptionHandler`, quando aplicável.

4. **Testes**

   * execução do comando `dotnet test`;
   * todos os testes aprovados;
   * `Failed: 0`.

Não devem ser incluídas credenciais reais ou informações sensíveis nas evidências.
