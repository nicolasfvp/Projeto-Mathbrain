using CalculaRanking.Utils;
using CalculaRanking.Models;

namespace Test.CalculaRanking;

public class Tests
{

    [Test]
    public void TestaRanking()
    {
        UserInfo userInfo = new UserInfo()
        {
            IdUsuario = "",
            RespostasCorretas = new bool[] { true, true, true, false, true, false, true, false },
            TempoDeResposta = new float[] { 2.0f, 2.0f, 5.0f, 0.5f, 0.5f, 0.2f, 1.0f, 0.5f }
        };
        var calcular = new CalcularRanking();
        UserInfoCalculado userInfoCalculado = new UserInfoCalculado();
        userInfoCalculado = calcular.Calcular(userInfo);
        Assert.AreEqual(userInfoCalculado.Ranking, 33040);
    }

    [Test]
    public void TestaBonusSequencia()
    {

        UserInfo userInfo = new UserInfo()
        {
            IdUsuario = "", 
            RespostasCorretas = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, },
            TempoDeResposta = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, }
        };
        var calcular = new CalcularRanking();
        UserInfoCalculado userInfoCalculado = new UserInfoCalculado();
        userInfoCalculado = calcular.Calcular(userInfo);
        Assert.AreEqual(userInfoCalculado.Ranking, 1242000);
    }

    [Test]
    public void TestaPontosPorTempo()
    {

        UserInfo userInfo = new UserInfo()
        {
            IdUsuario = "",
            RespostasCorretas = new bool[] { true, false, true, false, true, false, true },
            TempoDeResposta = new float[] { 0.0f, 1.0f, 5.0f, 1.0f, 10.0f, 1.0f, 20.0f },
        };
        var calcular = new CalcularRanking();
        UserInfoCalculado userInfoCalculado = new UserInfoCalculado();
        userInfoCalculado = calcular.Calcular(userInfo);
        Assert.AreEqual(userInfoCalculado.Ranking, 15000);
    }
    [Test]
    public void TestaTempo()
    {
        UserInfo userInfo = new UserInfo()
        {
            IdUsuario = "",
            RespostasCorretas = new bool[] { true, false, true, false,  },
            TempoDeResposta = new float[] { 0.5f, 0.1f, 5.0f, 1.0f,},
        };
        var calcular = new CalcularRanking();
        UserInfoCalculado userInfoCalculado = new UserInfoCalculado();
        userInfoCalculado = calcular.Calcular(userInfo);
        
        Assert.AreEqual(userInfoCalculado.MelhorTempo, 0.5);
    }
    [Test]
    public void TestaAcertos()
    {
        UserInfo userInfo = new UserInfo()
        {
            IdUsuario = "",
            RespostasCorretas = new bool[] { true, false, true, false, true, false, true },
            TempoDeResposta = new float[] { 0.0f, 1.0f, 5.0f, 1.0f, 20.0f, 1.0f, 2.0f },
        };
        var calcular = new CalcularRanking();
        UserInfoCalculado userInfoCalculado = calcular.Calcular(userInfo);
        Assert.AreEqual(userInfoCalculado.QtdAcertos, 4);
    }
}