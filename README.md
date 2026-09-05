# 🚚 LogiTracker - Sistema de Gestão Logística

Este projeto faz parte da avaliação **CP2 (Check Point 2)** do curso de **Análise e Desenvolvimento de Sistemas - FIAP**.
O objetivo é aplicar conceitos de:

* Clean Architecture
* Entity Framework Core
* Persistência de dados com banco relacional
* Migrações versionadas

---

## 👥 Integrantes

* **Nome:** Felipe Monte de Sousa **RM:** 562019
* **Nome:** Manuela de Lacerda Soares **RM:** 564887
---

## 🔗 Link Swagger

http://localhost:5138/swagger/index.html

## 🏗️ Domínio Escolhido

O sistema **LogiTracker** atua no domínio de **Logística e Transportes**, permitindo:

* Gerenciamento de transportadoras
* Controle de veículos e motoristas
* Rastreamento de entregas
* Associação entre carga, veículo e motorista

---

## 🧩 Entidades Modeladas

O modelo foi implementado em C# seguindo princípios de encapsulamento e separação de responsabilidades:

* **Carrier (Transportadora):** entidade central que gerencia veículos e motoristas
* **Vehicle (Veículo):** representa os veículos da frota
* **Driver (Motorista):** profissionais vinculados à transportadora
* **Cargo (Carga):** informações da carga transportada
* **Delivery (Entrega):** operação logística que conecta veículo, motorista e carga

---

## 🔄 Relacionamentos (MER)

| Relacionamento     | Cardinalidade | Descrição                                   |
| ------------------ | ------------- | ------------------------------------------- |
| Carrier → Vehicle  | 1:N           | Uma transportadora possui vários veículos   |
| Carrier → Driver   | 1:N           | Uma transportadora possui vários motoristas |
| Vehicle → Delivery | 1:N           | Um veículo pode realizar várias entregas    |
| Driver → Delivery  | 1:N           | Um motorista pode realizar várias entregas  |
| Cargo → Delivery   | 1:1           | Uma carga gera exatamente uma entrega       |

📌 O modelo foi implementado fielmente no **Entity Framework Core**, incluindo:

* Chaves primárias (PK)
* Chaves estrangeiras (FK)
* Controle de nulidade
* Relacionamentos explícitos via Fluent API

## Modelo Relacional
![Diagrama MER do Projeto](/docs/MER.jpg)

## Print Swagger
![Swagger](/docs/swaggerum.png)
![Swagger](/docs/swaggerdois.png)

## Print de ProblemDetails
![ProblemDetails](/docs/problem.jpg)
---

## 🗄️ Banco de Dados

* **SGBD utilizado:** Oracle
* Configuração via **Entity Framework Core Provider para Oracle**

📌 O banco pode ser recriado localmente utilizando as migrações.

---

## ⚙️ Persistência com EF Core

✔ `DbContext` localizado na camada **Infrastructure**
✔ Configuração via **Fluent API (`IEntityTypeConfiguration`)**
✔ Separação por entidade
✔ Relacionamentos explícitos e fiéis ao MER

---

## 🧬 Migrações

O projeto utiliza migrações versionadas do EF Core:

* Migration inicial criada
* Snapshot do modelo incluído

### ▶️ Comandos para executar:

```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
```

---

## 🧱 Arquitetura

O projeto segue o padrão **Clean Architecture**:

* **Domain:** Entidades e regras básicas
* **Application:** Interfaces de repositório
* **Infrastructure:** EF Core, DbContext e implementações
* **API:** Controllers e configuração de DI

---

## 🧠 Padrão de Repositório

✔ Interfaces definidas na camada **Application**
✔ Implementações na camada **Infrastructure**
✔ Injeção de dependência configurada no `Program.cs`:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
```

---

## 🔐 Configuração

A string de conexão está em:

```json
appsettings.json
```

📌 Dados sensíveis não são versionados (recomendado uso de User Secrets ou variáveis de ambiente).

---

## 🌐 Execução da API

### ▶️ Rodar o projeto:

Restaurar dependências
```bash
dotnet restore
```

Compilar
```bash
dotnet build
```

Executar a API
```bash
dotnet run --project LogiTracker.API
```
## Swagger
* Após executar a aplicação:
http://localhost:5138/swagger

### Endpoints disponíveis:
# Cargo
* GET /api/Cargo
* GET /api/Cargo/{id}
* POST /api/Cargo
* DELETE /api/Cargo/{id}
* 
# Carrier
* GET /api/Carrier
* GET /api/Carrier/{id}
* POST /api/Carrier
* DELETE /api/Carrier/{id}
* 
# Delivery
* GET /api/Delivery
* GET /api/Delivery/{id}
* POST /api/Delivery
* DELETE /api/Delivery/{id}

# Driver
* GET /api/Driver
* GET /api/Driver/{id}
* POST /api/Driver
* DELETE /api/Driver/{id}

# Vehicle
* GET /api/Vehicle
* GET /api/Vehicle/{id}
* POST /api/Vehicle
* DELETE /api/Vehicle/{id}

---

## 📁 Evidências

A pasta `/docs` contém:

* Print do banco de dados gerado
* Estrutura das tabelas
* Modelo MER atualizado
* Print Swagger
* Print ProblemDetails 

---

## 📌 Observações

* O projeto não contém regras de negócio complexas na camada de infraestrutura (conforme requisito da CP2)
* O foco está na persistência, mapeamento e organização em camadas
* O modelo foi ajustado para garantir consistência entre domínio e banco de dados

---

## 🌟 Propósito

> “Faça o seu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda”
> — Mario Sergio Cortella

---

## 🔗 Relação com o CP1

| CP1              | CP2                      |
| ---------------- | ------------------------ |
| MER conceitual   | Esquema físico no banco  |
| Entidades em C#  | Persistência com EF Core |
| Sem banco        | Banco configurado        |
| Sem repositórios | Repositórios + DI        |

---

# 📌 CP4 — Health Checks, Observabilidade e Testes com xUnit

## 👥 Integrantes (mesmo grupo do CP1–CP3)

* **Nome:** Felipe Monte de Sousa **RM:** 562019
* **Nome:** Manuela de Lacerda Soares **RM:** 564887

## 🗄️ Domínio e SGBD (herdados)

* **Domínio:** Logística e Transportes (Carrier, Vehicle, Driver, Cargo, Delivery).
* **SGBD:** Oracle, via `Oracle.EntityFrameworkCore` (mesmo `ApplicationDbContext` do CP2).

## ▶️ Como subir a API

```bash
dotnet restore
dotnet build
dotnet run --project LogiTracker.API
```

### URLs

* Swagger: http://localhost:5138/swagger
* Health check: http://localhost:5138/health

## 🩺 Health checks (`GET /health`)

Único endpoint de health check, com resposta em JSON (status geral, duração total e lista de checks com nome, status e duração; o campo `error` só é preenchido em **Development**).

| Check       | O que verifica                                                                 |
|-------------|---------------------------------------------------------------------------------|
| `self`      | O processo da API está no ar (`HealthCheckResult.Healthy`).                     |
| `oracle-db` | Conectividade com o banco Oracle, via `AddDbContextCheck<ApplicationDbContext>` (abordagem **A** do enunciado, alinhada ao `DbContext` do CP2). |

Registro feito em `LogiTracker.API/Extensions/HealthCheckServiceCollectionExtensions.cs` (método `AddLogiTrackerHealthChecks`), para não inchar o `Program.cs`. O writer JSON está em `LogiTracker.API/Health/HealthCheckResponseWriter.cs`.

**Status HTTP:** `Healthy` → 200 · `Degraded` → 200 · `Unhealthy` → 503 (comportamento padrão do middleware de health checks do ASP.NET Core).

**Validação:**
* Com a API e o Oracle acessíveis → `GET /health` retorna **200** com os dois checks `Healthy`.
* Com a connection string do Oracle inválida (ou o banco inacessível, em ambiente local) → `GET /health` retorna **503**, com o check `oracle-db` como `Unhealthy`.

## 📊 Observabilidade (logs)

* Logs estruturados via `ILogger<T>` nativo, com propriedades nomeadas (nunca concatenando string solta) e correlação por `traceId` (`HttpContext.TraceIdentifier`).
* Fluxo de escrita instrumentado: `POST /api/Delivery` (`DeliveryController.Create`) loga início e sucesso da criação da entrega, com `VehicleId`, `DriverId`, `CargoId`, `DeliveryId` e `TraceId`.
* `GlobalExceptionHandler` loga toda exceção não tratada em nível **Error**, incluindo o mesmo `traceId`; em **Development**, o `traceId` também é incluído em `ProblemDetails.Extensions`. Em **Production**, a resposta HTTP continua sem stack trace — o detalhe fica só no log.

## 🧪 Testes com xUnit

Dois projetos de teste na mesma solution:

* **`LogiTracker.Domain.Tests`** — referencia somente `LogiTracker.Domain`, sem mock (AAA explícito):
  * `CargoTests`: `[Fact]` no caminho feliz (carga criada ativa) e `[Theory]` + `[InlineData]` para peso/valor monetário inválidos.
  * `DeliveryTests`: `[Fact]` para transição válida de status e `[Theory]` + `[InlineData]` para a regra de negócio que impede alterar o status de uma entrega já `Delivered`/`Cancelled`.
* **`LogiTracker.Application.Tests`** — referencia `LogiTracker.Application`, com **Moq** dos repositórios (`IRepository<Vehicle>`, `IRepository<Driver>`, `IRepository<Cargo>`, `IDeliveryRepository`), testando o novo serviço de aplicação `DeliveryService`:
  * Cenário de dependência ausente (veículo, motorista **ou carga** inexistente) → lança `KeyNotFoundException` **e não chama** `IDeliveryRepository.Create` (`Times.Never`), com um `[Fact]` para cada uma das três dependências.
  * Caminho feliz → persiste a entrega uma única vez (`Times.Once`).

### ▶️ Rodar os testes

```bash
dotnet test
```

> ⚠️ Este README foi atualizado com base no código entregue; rode `dotnet test` no seu ambiente para confirmar que os testes estão todos verdes antes da entrega.

## 🧩 Novo componente de Application (CP4)

O CP3 não possuía uma camada de serviço de aplicação isolada — os controllers chamavam diretamente as interfaces de repositório (`ICargoRepository`, `IDeliveryRepository`, etc.), que já retornavam DTOs prontos. Para viabilizar o teste de Application com mock exigido pelo CP4 (cenário de dependência ausente sem persistir), foi adicionado:

* `IDeliveryService` / `DeliveryService` (`LogiTracker.Application/Services`): valida, via `IRepository<T>` (repositório genérico já existente desde o CP2), se o veículo, o motorista e a carga informados existem antes de delegar a criação ao `IDeliveryRepository`. Se qualquer dependência não existir, lança `KeyNotFoundException` e **não persiste**.
* `DeliveryController.Create` passou a chamar `IDeliveryService.CreateAsync`, mantendo o controller **sem `DbContext`** (Clean Architecture preservada) e adicionando os logs de início/sucesso com `traceId`.

Nenhum endpoint, controller, DTO, migration ou repositório do CP2/CP3 foi removido.

## 📋 Tabela de mapeamento de exceções (`GlobalExceptionHandler`)

| Exceção                     | Status HTTP | Título                  |
|------------------------------|:-----------:|--------------------------|
| `ResourceNotFoundException`  | 404         | Not Found                |
| `DomainException`            | 400         | Bad Request              |
| `ArgumentException`          | 400         | Bad Request              |
| `KeyNotFoundException`       | 404         | Not Found                |
| `InvalidOperationException`  | 409         | Conflict                 |
| Qualquer outra exceção       | 500         | Internal Server Error    |

Em **Development**, `Detail` traz `exception.Message` e o `traceId` é incluído em `ProblemDetails.Extensions`. Em **Production**, `Detail` traz uma mensagem genérica ("Erro interno na aplicação.") — nunca stack trace.

> 📝 **Nota sobre `DomainException` e `ResourceNotFoundException`:** essas duas classes já existiam em `LogiTracker.Domain/Exceptions` desde o CP3, mas nenhuma entidade ou repositório as lançava (o handler original também não as tratava explicitamente — caíam no `_ => 500`). No CP4 elas passaram a ser mapeadas explicitamente (400 e 404, respectivamente), deixando o handler pronto caso o time venha a lançá-las no domínio. Nenhum mapeamento existente do CP3 (`ArgumentException`, `KeyNotFoundException`, `InvalidOperationException`, default) foi alterado ou removido.

## 📁 Evidências (`/docs`)

Adicione em `/docs`, antes da entrega (veja `docs/EVIDENCIAS.md` para um passo a passo):

* Print ou trecho JSON de `GET /health` **Healthy** (200).
* Print ou trecho JSON de `GET /health` **Unhealthy** (503), com o Oracle parado ou connection string inválida localmente (sem commitar credencial real).
* Trecho de log (console) de um `POST /api/Delivery` com `traceId`, e de uma exceção tratada pelo `GlobalExceptionHandler`.
* Saída de `dotnet test` (ou print do Test Explorer) com todos os testes passando.

## ✅ Checklist rápido (CP4)

- [x] Migrations, `DbContext`, Swagger e `GlobalExceptionHandler` do CP2/CP3 intactos (mapeamentos originais preservados; apenas 2 casos novos adicionados).
- [x] `GET /health` implementado com checks `self` e `oracle-db`, JSON com cada check nomeado.
- [x] Status HTTP 200 (Healthy/Degraded) e 503 (Unhealthy) coerentes.
- [x] Log de um `POST` com propriedades nomeadas + `traceId`; exceção logada no handler com `traceId`.
- [x] `LogiTracker.Domain.Tests`: `[Fact]` + `[Theory]` em regras de domínio (Cargo e Delivery).
- [x] `LogiTracker.Application.Tests`: mock de repositórios + `Times.Never` para as 3 dependências (veículo, motorista, carga) + `Times.Once` no caminho feliz.
- [ ] `dotnet test` executado localmente e passando (confirme no seu ambiente).
- [ ] Evidências (`/docs`) e prints adicionados antes da entrega.

---
