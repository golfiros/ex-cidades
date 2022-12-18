using System;

class Program
{
    public static void Main()
    {
        uint numeroCidades = ValidarEntrada("Digite o número de cidades: ");
        uint[,] distancias = new uint[numeroCidades, numeroCidades];

        for (int cidade1 = 0; cidade1 < numeroCidades; cidade1++)
        {
            for (int cidade2 = cidade1 + 1; cidade2 < numeroCidades; cidade2++)
            {
                distancias[cidade1, cidade2] =
                    ValidarEntrada($"Dê a distância entre as cidades {cidade1 + 1} e {cidade2 + 1}: ");
                distancias[cidade2, cidade1] = distancias[cidade1, cidade2];
            }
        }

        uint tamanhoTrajeto = ValidarEntrada("Digite o número de cidades no percurso desejado: ");
        uint[] trajeto = new uint[tamanhoTrajeto];

        for (int parada = 0; parada < tamanhoTrajeto; parada++)
        {
            trajeto[parada] = ValidarEntrada($"Dê o número da parada {parada + 1}") - 1;
        }

        Console.Write("A distância total percorrida é de ");
        uint distanciaTotal = 0;
        for (int parada = 0; parada < tamanhoTrajeto - 1; parada++)
        {
            uint distancia = distancias[trajeto[parada], trajeto[parada + 1]];
            distanciaTotal += distancia;
            Console.Write($"{distancia} ");
            if (parada < tamanhoTrajeto - 2)
            {
                Console.Write("+ ");
            }
        }
        Console.WriteLine($"= {distanciaTotal} km.");
    }

    public static uint ValidarEntrada(string mensagem)
    {
        Console.WriteLine(mensagem);
        uint valorSaida;
        while (!uint.TryParse(Console.ReadLine(), out valorSaida))
        {
            Console.WriteLine("Entrada inválida, tente novamente: ");
        }
        return valorSaida;
    }
}
