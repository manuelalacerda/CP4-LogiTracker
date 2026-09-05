# 🚚 LogiTracker - Sistema de Gestão Logística (CP4)

Este projeto faz parte da avaliação **CP4 (Check Point 4)** do curso de **Análise e Desenvolvimento de Sistemas - FIAP**, evoluindo a API desenvolvida nos CPs anteriores com **Health Checks**, **Observabilidade (Logs estruturados)** e **Testes Automatizados (xUnit)**.

---

## 👥 Integrantes

* **Nome:** Manuela de Lacerda Soares **RM:** 564887
* **Nome:** Sofia Siqueira **RM:** 

---

## 🔗 URLs da Aplicação

* **Swagger UI:** `http://localhost:5138/swagger/index.html`
* **Health Check:** `http://localhost:5138/health`

---

## 🎯 Novidades da CP4

1. **Health Checks Operacionais (`GET /health`):**
   - Endpoint único configurado para validar o status do processo (**`self`**) e a conectividade com o banco de dados Oracle via Entity Framework Core (`AddDbContextCheck`).
   - Retorna um relatório formatado em **JSON** contendo o status geral, a duração total e os detalhes de cada check.
   - Status HTTP alinhados ao runtime: **200 OK** para sistemas saudáveis e **503 Service Unavailable** caso o banco de dados esteja inacessível.

2. **Observabilidade e Logs Estruturados:**
   - Instrumentação com `ILogger<T>` contendo o rastreamento de requisições através do `traceId` (`HttpContext.TraceIdentifier`).
   - Log de início e sucesso nos fluxos de escrita e tratamento centralizado de exceções no `GlobalExceptionHandler`.

3. **Testes Automatizados (xUnit & Moq):**
   - **`LogiTracker.Domain.Tests`**: Testes de unidade para as regras de negócio das entidades do domínio (sem mocks e sem infraestrutura), utilizando `[Fact]` e `[Theory]` com `[InlineData]`.
   - **`LogiTracker.Application.Tests`**: Testes de aplicação utilizando **Moq** nas interfaces de repositório para validar o comportamento em cenários de erro (garantindo que o método de persistência nunca seja acionado indevidamente com `Times.Never`).

---

## 🏗️ Domínio Escolhido

O sistema **LogiTracker** atua no domínio de **Logística e Transportes**, permitindo:

* Gerenciamento de transportadoras
* Controle de veículos e motoristas
* Rastreamento de entregas
* Associação entre carga, veículo e motorista

---

## 🧩 Entidades Modeladas

* **Carrier (Transportadora):** entidade central que gerencia veículos e motoristas
* **Vehicle (Veículo):** representa os veículos da frota
* **Driver (Motorista):** profissionais vinculados à transportadora
* **Cargo (Carga):** informações da carga transportada
* **Delivery (Entrega):** operação logística que conecta veículo, motorista e carga

---

## 🗄️ Banco de Dados e Migrações

* **SGBD utilizado:** Oracle
* Configurado via **Entity Framework Core** com migrações versionadas.

Para atualizar o banco de dados localmente:
```bash
dotnet ef database update --project LogiTracker.Infrastructure --startup-project LogiTracker.API
