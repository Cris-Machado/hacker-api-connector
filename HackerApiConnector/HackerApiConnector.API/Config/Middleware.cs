using HackerApiConnector.Domain.Exceptions;

namespace HackerApiConnector.API.Config
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Middleware> _logger;

        public Middleware(RequestDelegate next, ILogger<Middleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                _logger.LogWarning(ex, "Bad Request error");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Bad Request", details = ex.Message });
            }
            catch (NoContentException ex)
            {
                _logger.LogInformation(ex, "No Content response");
                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            catch (BadGatewayException ex)
            {
                _logger.LogError(ex, "Bad Gateway error");
                context.Response.StatusCode = StatusCodes.Status502BadGateway;
                await context.Response.WriteAsJsonAsync(new { error = "Bad Gateway", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Unexpected Error" });
            }
        }
    }
}
