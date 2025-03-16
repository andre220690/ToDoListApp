using Microsoft.AspNetCore.Http.Connections;
using NLog;
using NLog.Web;
using ToDoListApp.Api.Middleware;
using ToDoListApp.Dal;
using ToDoListApp.Domain;
using ToDoListApp.Domain.Hubs;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Initialization main");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services
    .AddDal(builder.Configuration)
    .AddDomain()
    .AddAuth(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
    option.AddPolicy("CorsPolice", builder => builder
        .WithOrigins("http://localhost", "http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
));

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolice");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.MapHub<TaskHub>("hub/task", option =>
{
    option.Transports = HttpTransportType.WebSockets;
});

app.Run();
