using Newtonsoft.Json;
using System.Text;

namespace ToDoListApp.Api.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = await FormatRequest(context);
        _logger.LogInformation("Request: {Request}", request);

        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            var response = await FormatResponse(context.Response);
            _logger.LogInformation("Response: {Response}", response);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> FormatRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            context.Items["Body"] = body;

            return $"Method: {context.Request.Method}, Path: {context.Request.Path}, QueryString: {context.Request.QueryString}, Body: {body}";
        }
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return $"StatusCode: {response.StatusCode}, Body: {text}";
    }

}
