using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Models
{
    public class EmProcesso
    {
        [Key]
        public int Id  {get; set;}
        public string IdUsuario { get; set;}
        public bool Processando { get; set;}
    }
}