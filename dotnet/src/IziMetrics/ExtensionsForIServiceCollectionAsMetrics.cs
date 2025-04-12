using IziHardGames.Observability.Contracts;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace IziMetrics
{
    public static class ExtensionsForIServiceCollectionAsMetrics
    {

        public static void AddGraphanaAndPrometheus(this IServiceCollection serviceCollection)
        {

        }
        // https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/docs/metrics/getting-started-prometheus-grafana/README.md
        // https://learn.microsoft.com/ru-ru/dotnet/core/diagnostics/observability-with-otel
        // https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/docs/metrics/getting-started-prometheus-grafana/Program.cs
        /// <example>
        /// app.UseOpenTelemetryPrometheusScrapingEndpoint();
        /// </example>
        public static OpenTelemetryBuilder AddMetricsWithSpecificOfIziHardGames(this OpenTelemetryBuilder builder, ObservabilityConfig config)
        {
            builder.WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(config.ServiceName))
                    .AddMeter("Microsoft.AspNetCore.Hosting")     // Metrics provides by ASP.NET Core in .NET 8
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddProcessInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddConsoleExporter()
                    //.AddPrometheusExporter(o =>
                    //{
                    //    //o.DisableTotalNameSuffixForCounters = true;
                    //})
                    .AddOtlpExporter()
                    ; // by default http://localhost:9090/api/v1/otlp/v1/metrics
            });
            return builder;
        }
    }
}
