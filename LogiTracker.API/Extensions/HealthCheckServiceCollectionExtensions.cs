using LogiTracker.Infrastructure.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LogiTracker.API.Extensions;

/// <summary>
/// Extensões para registrar os health checks da API do LogiTracker sem inchar o
/// <c>Program.cs</c>.
/// </summary>
public static class HealthCheckServiceCollectionExtensions
{
    /// <summary>
    /// Registra os checks expostos em <c>GET /health</c>:
    /// <list type="bullet">
    /// <item><description><c>self</c>: confirma que o processo da API está no ar.</description></item>
    /// <item><description><c>oracle-db</c>: valida a conectividade com o banco Oracle
    /// usado pelo <see cref="ApplicationDbContext"/> (mesmo banco do CP2/CP3).</description></item>
    /// </list>
    /// </summary>
    public static IServiceCollection AddLogiTrackerHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck(
                "self",
                () => HealthCheckResult.Healthy("O processo da API está no ar."),
                tags: ["ready"])
            .AddDbContextCheck<ApplicationDbContext>(
                name: "oracle-db",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready", "db"]);

        return services;
    }
}
