using Microsoft.AspNetCore.Connections;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using RabbitMQ.Client;
using RankingApi.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace RankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : Controller
    {
        private readonly RankingApiContext _context;
        private readonly ConnectionFactory _factory;
        public UserInfoController(RankingApiContext context)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "142.93.173.18",
                UserName = "admin",
                Password = "devintwitter"
            };
            _context = context;
        }

        // POST api/<UserInfoController>
        [HttpPost]
        public ActionResult Post([FromBody] UserInfo model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if(model.RespostasCorretas.Length != model.TempoDeResposta.Length)
            {
                return BadRequest();
            }
            var UserInfo = new UserInfo()
            {
                IdUsuario = model.IdUsuario,
                RespostasCorretas = model.RespostasCorretas,
                TempoDeResposta = model.TempoDeResposta

            };
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            try
            {
                PublicaUserInfo(UserInfo, channel);
                if(!EmProcessoExist(UserInfo.IdUsuario)){
                  EmProcesso emProcesso = new EmProcesso(){
                    IdUsuario = UserInfo.IdUsuario,
                    Processando = true,
                  };
                  _context.EmProcesso.Add(emProcesso);
                  _context.SaveChanges();
                }else{
                  EmProcesso DbEmProcesso = _context.EmProcesso.Where(x => x.IdUsuario == UserInfo.IdUsuario).FirstOrDefault();
                  DbEmProcesso.Processando = true;
                  _context.Entry(DbEmProcesso).State = EntityState.Modified;
                  _context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        private void PublicaUserInfo(UserInfo model, IModel channel)
        {
            channel.QueueDeclare(queue: "TesteProjetoNicolas",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
            arguments: null);

            var body = JsonConvert.SerializeObject(model);

            var modelBytes = Encoding.UTF8.GetBytes(body);
            channel.BasicPublish(exchange: string.Empty,
                            routingKey: "TesteProjetoNicolas",
                            basicProperties: null,
                            body: modelBytes);
        }

        private bool EmProcessoExist(string idUsuario){
          return _context.EmProcesso.Any(e => e.IdUsuario == idUsuario);
        }

    }
}
