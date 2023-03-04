using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeraNumeros.Models
{
    public class Operacoes
    {
        public string[] Operacao { get; set;}
        public float[] Resultado { get; set;}
        public Operacoes(string[] Op, float[] Re){
          Operacao = Op;
          Resultado = Re;
        }
    }
}