using System.Reflection;
using Evently.Api.Extensions;
using Evently.Api.Middleware;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure.Events;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => 
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

Assembly[] assembliesModules = [Evently.Modules.Events.Application.AssemblyReference.Assembly];

builder.Configuration.AddModuleConfiguration(assembliesModules);

builder.Services.AddApplication(assembliesModules);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("Database")!,
    builder.Configuration.GetConnectionString("Cache")!);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

EventsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

await app.RunAsync();
