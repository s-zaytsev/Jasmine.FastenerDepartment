using Prometheus;

namespace Jasmine.FastenerDepartment.WebApi.Middlewares;

/// <summary>
/// Metrics middleware.
/// </summary>
public class MetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Counter _requestCounter;
    private readonly Histogram _requestDuration;


    /// <summary>
    /// Creates middleware.
    /// </summary>
    public MetricsMiddleware(RequestDelegate next)
    {
        _next = next;

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

    /// <summary>
    /// Invokes.
    /// </summary>
    /// <param name="context">HTTP context.</param>
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