using Custom_Middleware.Service;

namespace Custom_Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILoggingService service;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger, ILoggingService service)
        {
            this.service = service;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string method = context.Request.Method;

            if (!service.CheckSequence(method))
            {
                _logger.LogError($"Sequence broke! Received {method} but expected order mismatch.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Error: API sequence is not maintained.");
                return;
            }

            _logger.LogInformation($"Sequence valid => {method}");
            await next(context);
        }
    }
}
