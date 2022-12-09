using System.Net;
using Entities.Exceptions;

namespace MeetApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case BadRequestException e:
                
                    await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, e.Message, error.Message); 
                    break;
                default:
                    await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "Непредвиденная ошибка на стороне сервера", error.Message);
                    break;
            }
            
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, string exceptionMessage)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogError(exceptionMessage);
        var responce = context.Response;
        responce.ContentType = "application/json";
        responce.StatusCode = (int) statusCode;
        await responce.WriteAsJsonAsync(message);
    }
}