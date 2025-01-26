using grpcApp.Application.Interfaces.IRepositories;
using grpcApp.Application.Interfaces.IServices;
using GrpcApp.Application.Services;
using grpcApp.Infrastructure.Repositories;
using GrpcApp.Infrastructure.Utility;
using grpcApp.Mappers;
using grpcApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
var configuration = builder.Configuration;
builder.Services.Configure<ContextSettings>(options =>
    options.ConnectionString = configuration.GetConnectionString("PostgresqlConnection"));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddAutoMapper(typeof(GrpcApp.Application.Mappers.UserMapper));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<GrpcApp.Infrastructure.AppContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserApiService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

