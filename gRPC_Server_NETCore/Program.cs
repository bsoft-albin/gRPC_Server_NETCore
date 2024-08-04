using gRPC.DbEngine;
using gRPC.Frameworks.CommonMeths;
using gRPC.Frameworks.CommonProps;
using gRPC.Repositories.Implementations;
using gRPC.Repositories.Interfaces;
using gRPC.Services.Implementations;
using gRPC.Services.Interfaces;
using gRPC_Server_NETCore.Services;
using Npgsql;
using System.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register your custom services and repositories
builder.Services.AddScoped<IDbConnection>((sp) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString(ConfigurationProps.Postgres_Connection_String);
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddScoped<IPostgresMapper, PostgresMapper>();
builder.Services.AddScoped<IPostgresRepo, PostgresRepo>();
builder.Services.AddScoped<IPostgresServices, PostgresServices>();

WebApplication app = builder.Build();

ErrorLogger.Initialize(app.Services.GetRequiredService<IHostEnvironment>());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

// Register only the required gRPC services.
// Remove unnecessary or duplicate service registrations.
app.MapGrpcService<UserServiceImpl>();

// Optional: If you still need GreeterService, uncomment the line below.
app.MapGrpcService<GreeterService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();
