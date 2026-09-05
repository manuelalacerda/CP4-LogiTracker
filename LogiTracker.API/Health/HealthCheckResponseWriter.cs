using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LogiTracker.API.Health;

/// <summary>
/// Serializa o <see cref="HealthReport"/> em um JSON legível para humanos e para
/// ferramentas de monitoramento, consumido pelo endpoint <c>GET /health</c>.
/// </summary>
public static class HealthCheckResponseWriter
{
    public static Task WriteJsonResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        // Detalhe de exceção só é exposto em Development (nunca em Production).
        var isDevelopment = context.RequestServices
            .GetRequiredService<IHostEnvironment>()
            .IsDevelopment();

        var payload = new
        {
            status = report.Status.ToString(),
            totalDurationMs = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                durationMs = entry.Value.Duration.TotalMilliseconds,
                error = isDevelopment ? entry.Value.Exception?.Message : null
            })
        };

        var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return context.Response.WriteAsync(json);
    }
}
