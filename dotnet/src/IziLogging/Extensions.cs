using IziHardGames.Observability.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace IziHardGames.Observability.Logging
{
    public static class Extensions
    {
        public static ILoggingBuilder AddLoggingWithOpenTelemetry(this ILoggingBuilder builder, ObservabilityConfig config)
        {
            builder.AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(config.ServiceName));
                // Defaults: 
                // OTLP/gRPC (default protocol): http://localhost:4317
                // OTLP/HTTP (Protobuf): http://localhost:4318
                logging.AddOtlpExporter();

                //logging.AddOtlpExporter(options =>
                //{
                //    options.Endpoint = new Uri("http://your-collector:4317");
                //    options.Protocol = OtlpExportProtocol.Grpc;
                //});
            });
            return builder;
        }

        public static OpenTelemetryBuilder AddLoggingWithOpenTelemetry(this OpenTelemetryBuilder builder, ObservabilityConfig config)
        {
            builder.WithLogging(log =>
            {
                log.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(config.ServiceName))
                   .AddOtlpExporter();
                //.AddOtlpExporter(opt =>
                //{
                //    opt.Endpoint = new Uri("http://localhost:4317");
                //    opt.Protocol = OtlpExportProtocol.Grpc;
                //    //opt.HttpClientFactory = () => new HttpClient(new HttpClientHandler
                //    //{
                //    //    ServerCertificateCustomValidationCallback = (_, _, _, _) => true // Disable TLS validation
                //    //});
                //});
            });
            return builder;
        }
    }
}
