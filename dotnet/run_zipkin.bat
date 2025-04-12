docker compose -f docker-compose.zipkin.yml up -d
::persistant
::setx GCE_OTLP_ENABLE_ZIPKIN=true
::setx OTEL_EXPORTER_ZIPKIN_ENDPOINT=http://otlp.com/api/v2/spans
set GCE_OTLP_ENABLE_ZIPKIN=true
set OTEL_EXPORTER_ZIPKIN_ENDPOINT=http://otlp.com/api/v2/spans
echo %GCE_OTLP_ENABLE_ZIPKIN%
echo %OTEL_EXPORTER_ZIPKIN_ENDPOINT%