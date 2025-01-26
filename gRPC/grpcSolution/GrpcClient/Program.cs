using Grpc.Net.Client;
using GrpcUserApi;

using var channel = GrpcChannel.ForAddress("http://localhost:5220");

var client = new GrpcUserApiService.GrpcUserApiServiceClient(channel);

Console.Write("Введите имя: ");
string? name = Console.ReadLine();

var replyCreate = client.CreateUser(new CreateUserRequest { Name = "Sanya", Age = 22 });
var replyGet = client.GetUser(new GetUserRequest() { Id = "c887c9c6-5d5d-445e-91cf-bb932886033d" });

Console.WriteLine($"Ответ сервера:");
Console.ReadKey();