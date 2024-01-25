namespace WebApplication1.Middlewares
{
	public class ResponseLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<BrowserCheckingMiddleware> _logger;
		public ResponseLoggingMiddleware(RequestDelegate next, ILogger<BrowserCheckingMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger ?? throw new ArgumentNullException(nameof(next));
		}
		public async Task InvokeAsync(HttpContext context)
		{
			_logger.LogInformation("Response Headers:\n{headers}\n=====\nResponse Body:\n{body}", context.Response.Headers, context.Response.Body);
			await _next(context);
		}
	}
}
