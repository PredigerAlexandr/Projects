using Grpc.Net.Client;
using grpcClient;
using grpcUserApiClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5220");

var client = new GrpcUserApiService.GrpcUserApiServiceClient(channel);

Console.Write("Введите имя: ");
string? name = Console.ReadLine();

var reply = await client.CreateUserAsync(new CreateUserRequest { Name = "Sanya", Age=22});

Console.WriteLine($"Ответ сервера: {reply.Name}");
Console.ReadKey();