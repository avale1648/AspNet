namespace WebApplication1.Middlewares
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestLoggingMiddleware> _logger;
		public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger ?? throw new ArgumentNullException(nameof(next));
		}
		public async Task InvokeAsync(HttpContext context)
		{
			_logger.LogInformation("Request Headers:\n{headers}\n=====\nRequest Body:\n{body}", context.Request.Headers, context.Request.Body);
			await _next(context);
		}
	}
}
