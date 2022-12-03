using System.Net;
using System.Text.Json;
using MeetApi.Exceptions;
using MeetApi.Models;

namespace MeetApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        var responce = context.Response;
        responce.ContentType = "application/json";
        responce.StatusCode = (int) statusCode;
        var errorDto = new ErrorDto()
        {
            Message = message,
            StatusCode = (int) statusCode
        };
        var result = errorDto.ToString();
        await responce.WriteAsJsonAsync(result);
    }
}