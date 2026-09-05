# 📁 Evidências do CP4 — passo a passo

Este arquivo é um roteiro para coletar rapidamente as evidências exigidas pelo
enunciado do CP4. Substitua cada seção pelo print real ou pelo trecho de
saída/JSON obtido no seu ambiente, e apague as instruções em itálico.

---

## 1. `GET /health` — Healthy (200)

1. Suba a API normalmente: `dotnet run --project LogiTracker.API`.
2. Com o Oracle acessível, acesse http://localhost:5138/health (ou use `curl`/Postman).
3. Cole aqui o JSON retornado (ou um print da tela) e confirme que o status HTTP foi **200**.

```
_(cole aqui o JSON de resposta, por exemplo:)_
{
  "status": "Healthy",
  "totalDurationMs": 12.3,
  "checks": [
    { "name": "self", "status": "Healthy", "description": "...", "durationMs": 0.1, "error": null },
    { "name": "oracle-db", "status": "Healthy", "description": null, "durationMs": 12.0, "error": null }
  ]
}
```

*Adicione também um print da tela do navegador/Postman, se possível (`health-healthy.png`).*

---

## 2. `GET /health` — Unhealthy (503)

1. Simule a indisponibilidade do banco **localmente** (não em produção):
   - Pare o serviço/instância Oracle, **ou**
   - Troque temporariamente `ConnectionStrings:DefaultConnection` em `appsettings.Development.json`
     (ou em `launchSettings.json`/User Secrets) por uma string inválida.
2. Reinicie a API e acesse `GET /health` novamente.
3. Confirme que o status HTTP retornado foi **503** e que o check `oracle-db` aparece como `Unhealthy`.
4. Cole aqui o JSON (ou print) e **desfaça a alteração da connection string** antes de commitar.

```
_(cole aqui o JSON de resposta com status Unhealthy)_
```

⚠️ **Não commite** a connection string real nem credenciais válidas — use um valor claramente inválido
apenas para o teste (ex.: `Data Source=host-invalido;User Id=x;Password=x;`).

---

## 3. Log de `POST /api/Delivery` com `traceId`

1. Com a API rodando, faça um `POST /api/Delivery` (Swagger, Postman ou `curl`) com um payload válido
   (`VehicleId`, `DriverId`, `CargoId` de registros já existentes).
2. Copie do console/terminal as duas linhas de log geradas pelo `DeliveryController.Create`
   (início e sucesso), que devem trazer o mesmo `TraceId`.

```
_(cole aqui as linhas de log, por exemplo:)_
info: LogiTracker.API.Controllers.DeliveryController[0]
      Iniciando criação de entrega. VehicleId: ..., DriverId: ..., CargoId: ..., TraceId: 0HN...
info: LogiTracker.API.Controllers.DeliveryController[0]
      Entrega criada com sucesso. DeliveryId: ..., TraceId: 0HN...
```

---

## 4. Log de exceção tratada pelo `GlobalExceptionHandler`

1. Provoque um erro tratado, por exemplo `GET /api/Delivery/{id}` com um `id` inexistente
   (dispara `KeyNotFoundException` → 404) ou `POST /api/Delivery` com um `VehicleId` inexistente
   (dispara a validação do `DeliveryService` → 404).
2. Copie do console a linha de log em nível `Error` gerada pelo `GlobalExceptionHandler`, com o `TraceId`.
3. Cole também a resposta JSON (`ProblemDetails`) recebida pelo cliente.

```
_(cole aqui a linha de log Error com TraceId)_
```

```json
_(cole aqui o ProblemDetails retornado, incluindo "traceId" se estiver em Development)_
```

---

## 5. Saída de `dotnet test`

1. Na raiz da solution, rode:

   ```bash
   dotnet test
   ```

2. Cole aqui a saída completa (ou um print do Test Explorer do Rider/Visual Studio),
   confirmando que **todos os testes passaram** (`LogiTracker.Domain.Tests` e
   `LogiTracker.Application.Tests`).

```
_(cole aqui a saída do dotnet test)_
```

---

✅ Depois de preencher as 5 seções acima com dados reais, marque o item correspondente
como concluído no checklist do `README.md` da raiz.
