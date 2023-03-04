
namespace GeraNumeros.Utils
{
    public class OperacoesMatematicas
    {
        private string[] operacoes;
        private float[] resultados;

        public OperacoesMatematicas()
        {
            operacoes = new string[50];
            resultados = new float[50];
        }

        public void GerarOperacoes()
        {
            Random rnd = new Random();
            int operador = 0;
            float resultado = 0;
            string operacao = "";

            for (int i = 0; i < operacoes.Length; i++)
            {
                int numero1;
                int numero2;

                if (i < 10)
                {
                    operador = rnd.Next(1, 3);

                }
                else if (i >= 10 && i < 30)
                {
                    operador = rnd.Next(1, 5);
                }
                else if (i >= 30)
                {
                    operador = rnd.Next(3, 5);
                }

                if (i < 10)
                {
                    numero1 = rnd.Next(1, 51);
                    numero2 = rnd.Next(1, 51);
                }
                else if (i >= 10 && i < 20)
                {
                    switch (operador)
                    {
                        case 1:
                            numero1 = rnd.Next(10, 501);
                            numero2 = rnd.Next(10, 501);
                            break;
                        case 2:
                            numero1 = rnd.Next(10, 501);
                            numero2 = rnd.Next(10, 501);
                            break;
                        case 3:
                            numero1 = rnd.Next(5, 31);
                            numero2 = rnd.Next(5, 31);
                            break;
                        case 4:
                            numero1 = rnd.Next(10, 201);
                            numero2 = rnd.Next(2, 51);
                            while(numero1 % numero2 != 0){
                              numero1 = rnd.Next(10, 201);
                              numero2 = rnd.Next(2, 51);
                            }
                            break;
                        default:
                            numero1 = rnd.Next(10, 501);
                            numero2 = rnd.Next(10, 501);
                            break;
                    }

                }
                else if (i >= 20 && i < 30)
                {
                    switch (operador)
                    {
                        case 1:
                            numero1 = rnd.Next(100, 2001);
                            numero2 = rnd.Next(100, 2001);
                            break;
                        case 2:
                            numero1 = rnd.Next(100, 2001);
                            numero2 = rnd.Next(100, 2001);
                            break;
                        case 3:
                            numero1 = rnd.Next(20, 201);
                            numero2 = rnd.Next(20, 201);
                            break;
                        case 4:
                            numero1 = rnd.Next(50, 501);
                            numero2 = rnd.Next(3, 101);
                            while(numero1 % numero2 != 0){
                              numero1 = rnd.Next(50, 501);
                              numero2 = rnd.Next(3, 101);
                            }
                            break;
                        default:
                            numero1 = rnd.Next(100, 2001);
                            numero2 = rnd.Next(100, 2001);
                            break;
                    }
                }
                else if (i >= 30 && i < 40)
                {
                    switch (operador)
                    {
                        case 3:
                            numero1 = rnd.Next(50, 501);
                            numero2 = rnd.Next(50, 501);
                            break;
                        case 4:
                            numero1 = rnd.Next(100, 1001);
                            numero2 = rnd.Next(10, 101);
                            while(numero1 % numero2 != 0){
                              numero1 = rnd.Next(100, 1001);
                              numero2 = rnd.Next(10, 101);
                            }
                            
                            break;
                        default:
                            numero1 = rnd.Next(50, 501);
                            numero2 = rnd.Next(50, 501);
                            break;
                    }
                }
                else if (i >= 40 && i <= 50)
                {
                    switch (operador)
                    {
                        case 3:
                            numero1 = rnd.Next(100, 5001);
                            numero2 = rnd.Next(100, 5001);
                            break;
                        case 4:
                            numero1 = rnd.Next(500, 10001);
                            numero2 = rnd.Next(10, 1001);
                            while(numero1 % numero2 != 0){
                              numero1 = rnd.Next(500, 10001);
                              numero2 = rnd.Next(10, 1001);
                            }
                            break;
                        default:
                            numero1 = rnd.Next(100, 5001);
                            numero2 = rnd.Next(100, 5001);
                            break;
                    }
                }
                else
                {
                    continue;
                }

                switch (operador)
                {
                    case 1:
                        resultado = numero1 + numero2;
                        operacao = $"{numero1} + {numero2} = ?";
                        break;
                    case 2:
                        resultado = numero1 - numero2;
                        operacao = $"{numero1} - {numero2} = ?";
                        break;
                    case 3:
                        resultado = numero1 * numero2;
                        operacao = $"{numero1} * {numero2} = ?";
                        break;
                    case 4:
                        resultado = float.Parse((numero1 / numero2).ToString("0.00"));
                        operacao = $"{numero1} / {numero2} = ?";
                        break;
                    default:
                        operacao = "Operação inválida";
                        resultado = 0;
                        break;
                }

                operacoes[i] = operacao;
                resultados[i] = resultado;



            }
        }

        public string[] GetOperacoes()
        {
            return operacoes;
        }

        public float[] GetResultados()
        {
            return resultados;
        }
    }
}
