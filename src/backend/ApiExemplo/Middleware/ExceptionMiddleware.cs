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
            //tenta fazer o proximo passo (Controller -> MediatR -> Handler)
            await _next(context);
        }
        catch (Exception ex)
        {
            // 2. Loga o erro
            _logger.LogError(ex, "Unhandled exception occurred.");

            // 3. Retorna um erro padronizado
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // status padrao (500)
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // monta o ProblemDetails
        var problem = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "Server Error",
            Title = "An internal server error has occurred.",
            Detail = exception.Message
        };

        var json = JsonSerializer.Serialize(problem);

        return context.Response.WriteAsync(json);
    }
}