using HospitalAPI.Common.Exceptions;
using HospitalAPI.Common.Models;
using System.Net;
using System.Text.Json;

namespace HospitalAPI.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            NotFoundException notFoundEx => new
            {
                statusCode = HttpStatusCode.NotFound,
                response = ApiResponse<object>.ErrorResponse(notFoundEx.Message)
            },
            TenantMismatchException tenantEx => new
            {
                statusCode = HttpStatusCode.Forbidden,
                response = ApiResponse<object>.ErrorResponse(tenantEx.Message)
            },
            _ => new
            {
                statusCode = HttpStatusCode.InternalServerError,
                response = ApiResponse<object>.ErrorResponse("An internal server error occurred")
            }
        };

        context.Response.StatusCode = (int)response.statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response.response));
    }
}
