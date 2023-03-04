using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeraNumeros.Models;
using GeraNumeros.Utils;

namespace GeraNumeros.Controllers
{
    [Route("api/GeraNumeros")]
    [ApiController]
    public class GeraNumerosController : ControllerBase
    {
        [HttpGet]

        public Operacoes Get(){

          OperacoesMatematicas operacoesMatematicas = new OperacoesMatematicas();
          operacoesMatematicas.GerarOperacoes();
          Operacoes operacoes = new Operacoes(operacoesMatematicas.GetOperacoes(), operacoesMatematicas.GetResultados());
          return operacoes;
        }
    }
}