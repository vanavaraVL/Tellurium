using FarmManagement.Dal.Exceptions;
using System.Net.Mime;
using System.Text.Json;

namespace FarmManagement.API.Middleware;

public class FarmerExceptionHandlingMiddleware
{
    private readonly RequestDelegate _nextDelegate;


    public FarmerExceptionHandlingMiddleware(RequestDelegate nextDelegate)
    {
        _nextDelegate = nextDelegate;
    }

    public virtual async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value!.Contains("swagger"))
        {
            await _nextDelegate.Invoke(context);
            return;
        }

        try
        {
            await _nextDelegate.Invoke(context);
        }
        catch (Exception ex) when (ex is EntityNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (Exception ex) when (ex is EntityUniqueException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception ex)
        {
            var errorInfo = GenerateErrorInfo(context, ex, StatusCodes.Status500InternalServerError);

            await context.Response.WriteAsync(errorInfo);
        }
    }

    private static string GenerateErrorInfo(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = statusCode;

        return JsonSerializer.Serialize(new { Message = "Sorry for inconvenience", exception.InnerException }, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}