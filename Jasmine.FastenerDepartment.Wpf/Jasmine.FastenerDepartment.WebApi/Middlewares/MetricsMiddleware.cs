using Prometheus;

namespace Jasmine.FastenerDepartment.WebApi.Middlewares;

public class MetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Counter _requestCounter;
    private readonly Histogram _requestDuration;

    public MetricsMiddleware(RequestDelegate next)
    {
        _next = next;

        // Создаем метрики Prometheus
        _requestCounter = Metrics.CreateCounter(
            "http_requests_total",
            "Total HTTP requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "path", "status_code" }
            });

        _requestDuration = Metrics.CreateHistogram(
            "http_request_duration_seconds",
            "HTTP request duration in seconds",
            new HistogramConfiguration
            {
                LabelNames = new[] { "method", "path", "status_code" }
            });
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var method = context.Request.Method;
            var path = GetNormalizedPath(context.Request.Path);
            var statusCode = context.Response.StatusCode.ToString();

            // Записываем метрики
            _requestCounter
                .WithLabels(method, path, statusCode)
                .Inc();

            _requestDuration
                .WithLabels(method, path, statusCode)
                .Observe(stopwatch.Elapsed.TotalSeconds);
        }
    }

    private static string GetNormalizedPath(PathString path)
    {
        // Нормализуем путь для группировки (например, /api/users/123 -> /api/users/{id})
        if (path.Value?.StartsWith("/api/") == true)
        {
            var segments = path.Value.Split('/');
            if (segments.Length > 3 && int.TryParse(segments[3], out _))
            {
                return $"/{segments[1]}/{segments[2]}/{{id}}";
            }
        }
        return path.Value ?? "/";
    }
}