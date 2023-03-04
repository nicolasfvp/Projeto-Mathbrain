using System.ComponentModel.DataAnnotations;

namespace CoreApi.Models
{
    public class UserRanking
    {
        [Key]
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public double MelhorRanking { get; set; }
        public double UltimoRanking { get; set; }
        public int QtdAcertos { get; set; }
        public float MelhorTempo { get; set; }
    }
}
