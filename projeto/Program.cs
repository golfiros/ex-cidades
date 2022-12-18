using System;
using System.IO;
using System.Linq;

class Program
{
    public static void Main()
    {
        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string caminhoDistancias = Path.Combine(caminhoDesktop, "matriz.txt");
        if (!File.Exists(caminhoDistancias))
        {
            Console.WriteLine("Arquivo de distâncias não encontrado.");
            return;
        }
        // muita coisa pode dar errado aqui então não
        // vou validar as entradas da matriz
        string[] linhasDistancia = File.ReadAllLines(caminhoDistancias);

        uint numeroCidades = (uint)linhasDistancia.Length;
        uint[,] distancias = new uint[numeroCidades, numeroCidades];

        for (int cidade1 = 0; cidade1 < numeroCidades; cidade1++)
        {
            string[] distanciasString = linhasDistancia[cidade1].Split(',');
            for (int cidade2 = 0; cidade2 < numeroCidades; cidade2++)
            {
                distancias[cidade1, cidade2] = uint.Parse(distanciasString[cidade2]);
            }
        }

        string caminhoTrajeto = Path.Combine(caminhoDesktop, "caminho.txt");
        if (!File.Exists(caminhoTrajeto))
        {
            Console.WriteLine("Arquivo de trajeto não encontrado.");
            return;
        }
        // mesma coisa aqui, sem validação
        string[] trajetoString = File.ReadLines(caminhoTrajeto).First().Split(',');

        uint tamanhoTrajeto = (uint)trajetoString.Length;
        uint[] trajeto = new uint[tamanhoTrajeto];

        for (int parada = 0; parada < tamanhoTrajeto; parada++)
        {
            trajeto[parada] = uint.Parse(trajetoString[parada]) - 1;
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
}
