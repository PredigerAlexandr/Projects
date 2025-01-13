using grpcApp.DataAccess.Utility;
using grpcApp.Mappers;
using grpcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var configuration = builder.Configuration;
builder.Services.Configure<ContextSettings>(options =>
    options.ConnectionString = configuration.GetConnectionString("PostgresqlConnection"));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddAutoMapper(typeof(GrpcApp.Application.Mappers.UserMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
