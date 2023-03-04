using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CalculaRanking.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

// Cria uma fila e publica o ranking processado

namespace CalculaRanking.Utils
{
    public class PublicaRanking
    {
        public void Publicar(UserInfoCalculado model)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "142.93.173.18",
                UserName = "admin",
                Password = "devintwitter"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "TesteProjetoNicolas2",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
            arguments: null);

            var body = JsonConvert.SerializeObject(model);

            var modelBytes = Encoding.UTF8.GetBytes(body);
            channel.BasicPublish(exchange: string.Empty,
                            routingKey: "TesteProjetoNicolas2",
                            basicProperties: null,
                            body: modelBytes);
        }

    }
    
}
