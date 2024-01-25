using Serilog;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApplication1.Services;
using WebApplication1.Middlewares;
//
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Server is running");
try
{
	var builder = WebApplication.CreateBuilder(args);
	builder.Host.UseSerilog((ctx, conf) =>
	{
		conf.MinimumLevel.Information()
		.WriteTo.Console()
		.MinimumLevel.Information();
	});
	builder.Services.AddSingleton<RedirectCountingService>();
	var app = builder.Build();
	app.UseMiddleware<BrowserCheckingMiddleware>();
	app.UseMiddleware<RequestLoggingMiddleware>();
	app.UseMiddleware<ResponseLoggingMiddleware>();
	app.UseMiddleware<RedirectCountingMiddleware>();
	app.MapGet("/", () => "Main page.");
	app.MapGet("/metrics", () => app.Services.GetService<RedirectCountingService>().GetPathCountPairs());
	app.Run();
}
catch(Exception ex)
{
	Log.Fatal(ex, "unexpected error");
}
finally
{
	Log.Information("Server is shutting down");
	await Log.CloseAndFlushAsync();
}