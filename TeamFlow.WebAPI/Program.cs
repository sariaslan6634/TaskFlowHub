// Program.cs
using Serilog;
using TeamFlow.WebAPI.Extensions;
using TeamFlow.WebAPI.Hubs;
using TeamFlow.WebAPI.Middleware;

// Serilog'u en başta kur
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("TeamFlow API başlatılıyor...");

    var builder = WebApplication.CreateBuilder(args);

    // Serilog'u appsettings.json'dan oku
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddDatabaseServices(builder.Configuration);
    builder.Services.AddIdentityServices();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddValidationServices();
    builder.Services.AddSignalRServices();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerServices();

    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(); // Her request otomatik loglanır

    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHub<NotificationHub>("/hubs/notification");
    app.MapHub<ChatHub>("/hubs/chat");
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "API beklenmedik şekilde durdu.");
}
finally
{
    Log.CloseAndFlush();
}