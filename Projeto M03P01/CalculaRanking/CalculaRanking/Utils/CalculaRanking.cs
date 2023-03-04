using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculaRanking.Models;

// Calcula o ranking relativo ao tempo de resposta, também retorna a quantidade de acertos e o melhor tempo de resposta.

namespace CalculaRanking.Utils
{
    public class CalcularRanking
    {
        public double RankingTotal { get; private set; }
        public int TotalAcertos { get; private set; }
        public float MenorTempo { get; private set; }
        public int MelhorSequencia { get; private set; }
        public UserInfoCalculado Calcular(UserInfo userInfo)
        {

            float menorTempo = float.MaxValue;

            for (int i = 0; i < userInfo.RespostasCorretas.Length; i++)
            {
                if (userInfo.RespostasCorretas[i])
                {
                    double pontuacao = new double();
                    if (userInfo.TempoDeResposta[i] <= 5)
                    {
                        pontuacao = 8000 - (userInfo.TempoDeResposta[i] * 800);
                    } 
                    else if(userInfo.TempoDeResposta[i] <= 10)
                    {
                        pontuacao = 4000 - ((userInfo.TempoDeResposta[i] - 5) * 400);
                    } 
                    else if(userInfo.TempoDeResposta[i] <= 20)
                    {
                        pontuacao = 2000 - ((userInfo.TempoDeResposta[i] - 10) * 100);
                    }
                    double bonusDeSequencia = (pontuacao / 10) * MelhorSequencia;
                    
                    pontuacao += bonusDeSequencia;
                    RankingTotal += pontuacao;
                    TotalAcertos++;
                    MelhorSequencia++;
                }
                else
                {
                    MelhorSequencia = 0;
                }

                if (userInfo.TempoDeResposta[i] != 0 && userInfo.TempoDeResposta[i] < menorTempo && userInfo.RespostasCorretas[i])
                {
                    menorTempo = userInfo.TempoDeResposta[i];
                }
            }

            MenorTempo = menorTempo;

            UserInfoCalculado userInfoCalculado = new UserInfoCalculado()
            {
                IdUsuario = userInfo.IdUsuario,
                Ranking = RankingTotal,
                QtdAcertos = TotalAcertos,
                MelhorTempo = MenorTempo
            };
            return userInfoCalculado;
        }
    }
}
