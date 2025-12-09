using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 1. Let the request go through to the Controller -> MediatR -> Handler
            await _next(context);
        }
        catch (Exception ex)
        {
            // 2. If ANY exception escapes the Handler, catch it here.
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Default to 500 Internal Server Error
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Use the standard "ProblemDetails" format (RFC 7807)
        var problem = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "Server Error",
            Title = "An internal server error has occurred.",
            Detail = exception.Message // In Prod, you usually hide this detail!
        };

        var json = JsonSerializer.Serialize(problem);

        return context.Response.WriteAsync(json);
    }
}