using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DbWrite.Models
{
    public class UserRanking
    {   
        [Key]
        public int Id {get; set;}
        public string IdUsuario { get; set; }
        public double MelhorRanking { get; set; }
        public double UltimoRanking { get; set; }
        public int QtdAcertos { get; set; }
        public float MelhorTempo { get; set; }
    }
}
