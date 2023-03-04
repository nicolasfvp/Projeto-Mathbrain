using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using DbWrite.Models;
using System.Threading.Channels;
using DbWrite;
using Microsoft.EntityFrameworkCore;

// Responsável por adicionar o processo ao banco de dados e atualizar a tabela de processamento para finalizado.

var factory = new ConnectionFactory()
{
    HostName = "142.93.173.18",
    UserName = "admin",
    Password = "devintwitter"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
using var context = new DbWriteContext();

channel.QueueDeclare(queue: "TesteProjetoNicolas2",
                           durable: true,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);




Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    UserInfoCalculado userInfoCalculado = JsonConvert.DeserializeObject<UserInfoCalculado>(message);
    UserRanking userRanking = new UserRanking()
    {
        IdUsuario = userInfoCalculado.IdUsuario,
        MelhorRanking = userInfoCalculado.Ranking,
        UltimoRanking = userInfoCalculado.Ranking,
        QtdAcertos = userInfoCalculado.QtdAcertos,
        MelhorTempo = userInfoCalculado.MelhorTempo
    };
    if (!UserRankingExists(userRanking.IdUsuario))
    {
        PostUserRanking(userRanking);
        Console.WriteLine($"Usuário de ID: {userRanking.IdUsuario} Adicionado ao Ranking.");
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }else{
      UpdateUserRanking(userRanking);
      Console.WriteLine($"Usuário de ID: {userRanking.IdUsuario} Teve seu Ranking Atualizado.");

      channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }

    
};

channel.BasicConsume(queue: "TesteProjetoNicolas2",
                        autoAck: false,
                        consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

 bool UserRankingExists(string id)
{
    return (context.UserRanking.Any(e => e.IdUsuario == id));
}

 async void PostUserRanking(UserRanking userRanking)
{
    EmProcesso DbEmProcesso = await context.EmProcesso.Where(x => x.IdUsuario == userRanking.IdUsuario).FirstAsync();

    context.UserRanking.Add(userRanking);

    DbEmProcesso.Processando = false;
    context.Entry(DbEmProcesso).State = EntityState.Modified;
    await context.SaveChangesAsync();
}

 async void UpdateUserRanking(UserRanking userRanking)
{
    UserRanking DbUserRanking = await context.UserRanking.Where(x => x.IdUsuario == userRanking.IdUsuario).FirstAsync();
    EmProcesso DbEmProcesso = await context.EmProcesso.Where(x => x.IdUsuario == userRanking.IdUsuario).FirstAsync();
    if(DbUserRanking.MelhorRanking < userRanking.MelhorRanking){
      DbUserRanking.MelhorRanking = userRanking.MelhorRanking;
    }
    if(DbUserRanking.MelhorTempo > userRanking.MelhorTempo){
      DbUserRanking.MelhorTempo = userRanking.MelhorTempo;
    }
    if(DbUserRanking.QtdAcertos < userRanking.QtdAcertos){
      DbUserRanking.QtdAcertos = userRanking.QtdAcertos;
    }
    DbUserRanking.UltimoRanking = userRanking.UltimoRanking;
    context.Entry(DbUserRanking).State = EntityState.Modified;

    DbEmProcesso.Processando = false;
    context.Entry(DbEmProcesso).State = EntityState.Modified;
    await context.SaveChangesAsync();

}