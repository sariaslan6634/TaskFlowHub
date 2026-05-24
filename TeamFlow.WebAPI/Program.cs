// Program.cs
using TeamFlow.WebAPI.Extensions;
using TeamFlow.WebAPI.Middlewar;

var builder = WebApplication.CreateBuilder(args);

// Servisler
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware sırası önemli
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Önce kim olduğunu anla
app.UseAuthorization();  // Sonra ne yapabileceğine bak
app.MapControllers();

app.Run();