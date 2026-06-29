using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;

namespace Jasmine.FastenerDepartment.WebApi.Middlewares;

/// <summary>
/// Error handling middleware.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger;
    private ILanguageService _languageService;

    /// <summary>
    /// Creates error handling middleware.
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ErrorHandlingMiddleware(
        RequestDelegate next,
        Serilog.ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes.
    /// </summary>
    /// <param name="context">HTTP context.</param>
    /// <param name="languageService">Language service.</param>
    public async Task InvokeAsync(HttpContext context, ILanguageService languageService)
    {
        try
        {
            _languageService = languageService;
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex,
                "Unhandled exception occurred: {ExceptionType} for {HttpMethod} {HttpPath} | " +
                "User: {UserId} | TraceId: {TraceId} | " +
                "ClientIP: {ClientIP} | UserAgent: {UserAgent}",
                ex.GetType().Name,
                context.Request.Method,
                context.Request.Path,
                context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous",
                context.TraceIdentifier,
                context.Connection.RemoteIpAddress?.ToString(),
                context.Request.Headers["User-Agent"].ToString());

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context, exception);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var problemDetails = new ProblemDetails
        {
            Type = GetProblemType(exception),
            Title = GetTitle(exception),
            Status = statusCode,
            Detail = GetUserFriendlyMessage(exception),
            Instance = context.Request.Path
        };

        return problemDetails;
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ValidationException => StatusCodes.Status400BadRequest,
        NotFoundException => StatusCodes.Status404NotFound,
        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
        DomainException => StatusCodes.Status422UnprocessableEntity,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetProblemType(Exception exception) => exception switch
    {
        ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        NotFoundException => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        UnauthorizedAccessException => "https://tools.ietf.org/html/rfc7235#section-3.1",
        DomainException => "https://tools.ietf.org/html/rfc4918#section-11.2",
        _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    };

    private static string GetTitle(Exception exception) => exception switch
    {
        ValidationException => "Validation exception",
        NotFoundException => "Resource not found.",
        UnauthorizedAccessException => "Access denied",
        DomainException => "Domain exception",
        _ => "Server exception"
    };

    private string GetUserFriendlyMessage(Exception exception)
    {
        if (exception is DomainException domainException && domainException.UserMessage is not null)
        {
            return domainException.UserMessage.GetText(_languageService.LanguageCode);
        }

        var message = exception switch
        {
            ValidationException => "Data incorrect",
            NotFoundException => "Resource not found",
            UnauthorizedAccessException => "Access denied",
            DomainException => "Impossible to complete operation",
            _ => "Something went wrong"
        };

        return message;
    }
}