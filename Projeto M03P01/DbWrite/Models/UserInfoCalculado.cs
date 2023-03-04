using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWrite.Models
{
    public class UserInfoCalculado
    {
      
        public string IdUsuario { get; set; }
        public double Ranking { get; set; }
        public int QtdAcertos { get; set; }
        public float MelhorTempo { get; set; }


    }
}
