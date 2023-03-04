namespace RankingApi.Models
{
    public class UserInfo
    {
        public string IdUsuario { get; set; }
        public bool[] RespostasCorretas { get; set; }
        public float[] TempoDeResposta { get; set; }

    }
}
