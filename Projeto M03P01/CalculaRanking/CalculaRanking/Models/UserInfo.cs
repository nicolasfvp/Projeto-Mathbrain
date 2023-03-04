using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculaRanking.Models
{
    public class UserInfo
    {
        public string IdUsuario { get; set; }
        public bool[] RespostasCorretas { get; set; }
        public float[] TempoDeResposta { get; set; }
    }
}
