// See https://aka.ms/new-console-template for more information

using MassTransit;
using RabbitMq;


var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq://localhost"); // Укажите адрес вашего RabbitMQ

    // Настройка exchange типа headers
    cfg.Message<YourMessage>(x =>
    {
        x.SetEntityName("your_headers_exchange");
    });
});

// Запускаем шину
await busControl.StartAsync();
try
{
    // Отправляем сообщение
    await busControl.Publish(new YourMessage { Text = "Hello, Headers Exchange!" });
    Console.WriteLine("Сообщение отправлено в headers exchange!");
}
finally
{
    // Останавливаем шину
    await busControl.StopAsync();
}

