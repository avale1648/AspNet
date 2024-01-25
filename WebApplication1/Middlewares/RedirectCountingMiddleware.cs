using WebApplication1.Services;
namespace WebApplication1.Middlewares
{
	public class RedirectCountingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<BrowserCheckingMiddleware> _logger;
		private readonly RedirectCountingService _redirectCountingService;
		public RedirectCountingMiddleware(RequestDelegate next, ILogger<BrowserCheckingMiddleware> logger, RedirectCountingService redirectCountingService)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger?? throw new ArgumentNullException(nameof(logger));
			_redirectCountingService = redirectCountingService ?? throw new ArgumentNullException(nameof(redirectCountingService));
		}
		public async Task InvokeAsync(HttpContext context)
		{
			_redirectCountingService.AddOrUpdate(context.Request.Path);
			await _next(context);
		}
	}
}
