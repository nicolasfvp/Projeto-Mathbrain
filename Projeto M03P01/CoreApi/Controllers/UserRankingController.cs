using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreApi.Context;
using CoreApi.Models;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRankingController : ControllerBase
    {
        private readonly MathBrainRankingContext _context;

        public UserRankingController(MathBrainRankingContext context)
        {
            _context = context;
        }

        // GET: api/GetUserRanking
        [HttpGet]
        [Route("GetUserRanking")]
        public async Task<ActionResult<IEnumerable<UserRanking>>> GetUserRanking()
        {
          if (_context.UserRanking == null)
          {
              return NotFound();
          }
          
          return await _context.UserRanking.ToListAsync();
        }

        // GET: api/GetUserRankingId/
        [HttpGet]
        [Route("GetUserRankingId")]
        public async Task<ActionResult<UserRanking>> GetUserRankingId([FromQuery] string id)
        {
          if (_context.UserRanking == null)
          {
              return NotFound();
          }
          UserRanking userRanking = await _context.UserRanking.Where(x => x.IdUsuario == id).FirstAsync();

          if (userRanking == null)
          {
               return NotFound();
          }

          return userRanking;
        }

        [HttpGet]
        [Route("GetProcessamento")]
        public async Task<ActionResult<EmProcesso>> GetProcessamento ([FromQuery] string id)
        {
          if(_context.EmProcesso == null){
            return NotFound();
          }
          EmProcesso emProcesso = await _context.EmProcesso.Where(x => x.IdUsuario == id).FirstAsync();
          if (emProcesso == null){
            return NotFound();
          }

          return emProcesso;
        }

    }
}
