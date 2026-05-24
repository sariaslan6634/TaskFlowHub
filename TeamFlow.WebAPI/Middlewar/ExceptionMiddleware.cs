// Middleware/ExceptionMiddleware.cs
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace TeamFlow.WebAPI.Middleware;

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
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Hata oluştu. Path: {Path} Method: {Method}",
                context.Request.Path,
                context.Request.Method,
                ex.InnerException?.Message); 

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            DbUpdateException dbEx => (HttpStatusCode.InternalServerError,
                dbEx.InnerException?.Message ?? dbEx.Message),
            _ => (HttpStatusCode.InternalServerError, "Sunucu hatası oluştu.")
        };


        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}