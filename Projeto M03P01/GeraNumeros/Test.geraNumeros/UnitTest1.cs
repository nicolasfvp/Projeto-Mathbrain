using System.Security.Cryptography.X509Certificates;
using GeraNumeros.Utils;
namespace Test.geraNumeros;

public class Tests
{
    public string[] Operacoes { get; set; }
    public float[] Resultados { get; set; }

    [Test]
    public void TestaOperacoes()
    {
        OperacoesMatematicas operacoesMatematicas = new OperacoesMatematicas();
        operacoesMatematicas.GerarOperacoes();
        Operacoes = operacoesMatematicas.GetOperacoes();
        bool TemErro = false;
        for(int i = 0;i < Operacoes.Length; i++)
        {
            if (Operacoes[i] == "")
            {
                TemErro = true;
            }
        }
        Assert.IsFalse(TemErro);
    }
    [Test] 
    public void TestaResultado()
    {
        OperacoesMatematicas operacoesMatematicas = new OperacoesMatematicas();
        operacoesMatematicas.GerarOperacoes();
        Operacoes = operacoesMatematicas.GetOperacoes();
        Resultados = operacoesMatematicas.GetResultados();
        float[] ConferirResultados = new float[50];
        for(int i = 0; i < Operacoes.Length; i++)
        {
            string[] Operacao = Operacoes[i].Split(' ');
            float resultado = 0.0f;
            if (Operacao[1] == "+")
            {
                resultado = float.Parse(Operacao[0]) + float.Parse(Operacao[2]);
            }
            if (Operacao[1] == "-")
            {
                resultado = float.Parse(Operacao[0]) - float.Parse(Operacao[2]);
            }
            if (Operacao[1] == "/")
            {
                resultado = float.Parse((int.Parse(Operacao[0]) / int.Parse(Operacao[2])).ToString("0.00"));
            }
            if (Operacao[1] == "*")
            {
                resultado = float.Parse(Operacao[0]) * float.Parse(Operacao[2]);
            }
            ConferirResultados[i] = resultado;
        }
        Assert.AreEqual(ConferirResultados, Resultados);
    }
}