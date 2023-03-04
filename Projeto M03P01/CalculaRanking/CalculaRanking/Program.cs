using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CalculaRanking.Models;
using CalculaRanking.Utils;

using static System.Net.Mime.MediaTypeNames;

// recebe o objeto da fila, processa e publica para a próxima fila (consumidor: DbWrite)

var factory = new ConnectionFactory()
{
    HostName = "142.93.173.18",
    UserName = "admin",
    Password = "devintwitter"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: "TesteProjetoNicolas",
                           durable: true,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(message);

    UserInfoCalculado userInfoCalculado = new UserInfoCalculado();

    try
    {
        var calcular = new CalcularRanking();
        userInfoCalculado = calcular.Calcular(userInfo);

        Console.WriteLine($"Ranking: {userInfoCalculado.Ranking}\n Quantidade de acertos: {userInfoCalculado.QtdAcertos}\n Melhor Tempo: {userInfoCalculado.MelhorTempo}");

        var publicar = new PublicaRanking();
        publicar.Publicar(userInfoCalculado);
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

    

};

channel.BasicConsume(queue: "TesteProjetoNicolas",
                        autoAck: false,
                        consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
