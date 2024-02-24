using Homework_2023_09_07.Config;
using Homework_2023_09_07.EmailSender;
using Microsoft.Extensions.Options;
using Serilog;
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Server started");
try
{
	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddOptions<SmtpConfig>().BindConfiguration("SmtpConfig").ValidateDataAnnotations();
	builder.Host.UseSerilog((ctx, config) => config.MinimumLevel.Information().WriteTo.Console().MinimumLevel.Information());
	builder.Services.AddControllers();
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
	var app = builder.Build();
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});
	app.MapGet("/", async (IEmailSender sender, CancellationToken token) =>
	{
		try
		{
			await sender.SendAsync(token);
			Log.Information("Message delivered");
		}
		catch(Exception ex)
		{
			Log.Fatal(ex.Message);
		}
		finally
		{
		}
	});
	app.Run();
}
catch(Exception ex)
{
	Log.Fatal(ex.Message);
}
finally
{
	Log.Information("Server shutted down");
	await Log.CloseAndFlushAsync();
}