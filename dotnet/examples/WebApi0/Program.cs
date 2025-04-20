
using IziHardGames.Observing.Tracing;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.EntityFrameworkCore;
using IziMetrics;
using WebApi0.BackgroundServices;
using Scalar.AspNetCore;
using IziHardGames.Observability.Logging;
using IziHardGames.Observability.Contracts;
using OpenTelemetry;

namespace WebApi0
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContextPool<SimpleDbContext>(x => x.UseInMemoryDatabase("iziinmemory"));
            var evUser = Environment.GetEnvironmentVariable("IZHG_DB_POSTGRES_USER_DEV");
            var evPwd = Environment.GetEnvironmentVariable("IZHG_DB_POSTGRES_PASSWORD_DEV");
            var evServer = Environment.GetEnvironmentVariable("IZHG_DB_POSTGRES_SERVER_DEV");
            var evPort = Environment.GetEnvironmentVariable("IZHG_DB_POSTGRES_PORT_DEV");
            builder.Services.AddDbContextPool<SimpleDbContext>(x => x.UseNpgsql($"server={evServer};uid={evUser};pwd={evPwd};database=IziTest;Include Error Detail=true"));

            builder.Services.AddHostedService<SomeGarbageProdcuer>();
            builder.Services.AddHostedService<LogSpammer>();

            //builder.Logging.AddOpenTelemetry(logging =>
            //{
            //    logging.IncludeFormattedMessage = true;
            //    logging.IncludeScopes = true;
            //});

            //var otel = builder.Services.AddOpenTelemetry();
            var env = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT");
            var config = new ObservabilityConfig() { ServiceName = "Example-otel" };
            //var otel = builder.Services.AddOpenTelemetry().AddLoggingWithOpenTelemetry(config);
            var otel = builder.Services.AddOpenTelemetry()
                     .AddMetricsWithSpecificOfIziHardGames(config)
                     .AddLoggingWithOpenTelemetry(config)
                     .AddTracesWithSpecificOfIziHardGames(config, new OtlpParams()
                     {
                         HostName = "localhost",
                         ServiceName = "IziTestOtlpTracingsService",
                         MainSourceName = "IziTestOtlpTracingsSource",
                         SubSourcesNames = new string[] { ActivityExample.SourceName }
                     });

            //builder.Logging.AddLoggingWithOpenTelemetry();
            // already called
            //otel.UseOtlpExporter();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "/openapi/{documentName}.json";
                });
                app.MapScalarApiReference();
            }

            app.UseAuthorization();


            app.MapControllers();
            //app.UseOpenTelemetryPrometheusScrapingEndpoint();
            app.Run();
        }
    }
}
