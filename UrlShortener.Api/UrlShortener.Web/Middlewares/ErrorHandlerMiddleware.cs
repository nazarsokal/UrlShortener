using UrlShortener.Application.Exceptions;

namespace UrlShortener.Web.Middlewares;

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly bool _isDevelopment;

    public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _isDevelopment = env.IsDevelopment();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var title = "Internal Server Error";
        object? errors = null;

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                title = "Validation Failed";
                errors = validationEx.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
                break;
            case NotFoundException: 
                statusCode = HttpStatusCode.NotFound;
                title = "Not Found";
                break;
            case DbUpdateException:
                statusCode = HttpStatusCode.Conflict;
                title = "Url Duplicate Error";
                break;
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                title = "Unauthorized Access";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            Status = context.Response.StatusCode,
            Title = title,
            Detail = exception.Message,
            Instance = context.Request.Path,
            TraceId = context.TraceIdentifier,
            Errors = errors,
            StackTrace = _isDevelopment ? exception.StackTrace : null,
            InnerException = _isDevelopment ? exception.InnerException?.Message : null
        };

        var jsonOptions = new JsonSerializerOptions 
        { 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
        };
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}