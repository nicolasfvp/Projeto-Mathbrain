using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DbWrite.Models
{
    public class EmProcesso
    {
        [Key]
        public int Id { get; set;}
        public string IdUsuario { get; set;}
        public bool Processando { get; set;}
        
      
    }
}