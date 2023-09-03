using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            // Handle specific exception types and return custom responses.
            if (ex is UnauthorizedAccessException)
            {
                await HandleUnauthorizedAccessException(context);
            }
            else if (ex is NotFoundException)
            {
                await HandleNotFoundException(context);
            }
            else
            {
                await HandleGenericException(context);
            }
        }
    }

    private async Task HandleUnauthorizedAccessException(HttpContext context)
    {
        var response = new { error = "Unauthorized access." };
        var jsonResponse = JsonSerializer.Serialize(response);

        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonResponse);
    }

    private async Task HandleNotFoundException(HttpContext context)
    {
        var response = new { error = "Resource not found." };
        var jsonResponse = JsonSerializer.Serialize(response);

        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonResponse);
    }

    private async Task HandleGenericException(HttpContext context)
    {
        var response = new { error = "An error occurred." };
        var jsonResponse = JsonSerializer.Serialize(response);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonResponse);
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
