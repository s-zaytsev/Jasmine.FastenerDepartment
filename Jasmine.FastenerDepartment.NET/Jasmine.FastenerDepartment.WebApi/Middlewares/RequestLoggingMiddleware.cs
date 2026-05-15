namespace Jasmine.FastenerDepartment.WebApi.Middlewares;

/// <summary>
/// Request logging middleware.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger;

    /// <summary>
    /// Creates middleware.
    /// </summary>
    /// <param name="next">Next delegate.</param>
    /// <param name="logger">Logger.</param>
    public RequestLoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes.
    /// </summary>
    /// <param name="context">HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            _logger.Information("HTTP {HttpMethod} {HttpPath} started",
                context.Request.Method, context.Request.Path);

            await _next(context);
            stopwatch.Stop();

            _logger.Information("HTTP {HttpMethod} {HttpPath} completed {StatusCode} in {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.Error(ex,
                "HTTP {HttpMethod} {HttpPath} failed with {StatusCode} in {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}
