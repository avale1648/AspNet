namespace WebApplication1.Middlewares
{
	public class BrowserCheckingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<BrowserCheckingMiddleware> _logger;
		public BrowserCheckingMiddleware(RequestDelegate next, ILogger<BrowserCheckingMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger ?? throw new ArgumentNullException(nameof(next));
		}
		public async Task InvokeAsync(HttpContext context)
		{
			var userAgentHeader = context.Request.Headers.UserAgent;
			var userAgent = userAgentHeader.ToString();
			if (userAgent.Contains("Edg"))
			{
				await _next(context);
			}
			else
			{
				await context.Response.WriteAsync("This app is available only in Edge Browser.");
			}
		}
	}
}
