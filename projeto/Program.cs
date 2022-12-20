using System;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    public static void Main()
    {
        //
        CsvConfiguration configuracaoCsv = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            NewLine = Environment.NewLine
        };

        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string caminhoDistancias = Path.Combine(caminhoDesktop, "matriz.txt");
        if (!File.Exists(caminhoDistancias))
        {
            Console.WriteLine("Arquivo de distâncias não encontrado.");
            return;
        }

        uint numeroCidades;
        uint[,] distancias;
        using (StreamReader leitorDistancias = new StreamReader(caminhoDistancias))
        using (CsvParser analisadorDistancias = new CsvParser(leitorDistancias, configuracaoCsv))
        {
            if (!analisadorDistancias.Read()) { return; }
            numeroCidades = (uint)(analisadorDistancias.Record.Length);
            distancias = new uint[numeroCidades, numeroCidades];
            for (int cidade1 = 0; cidade1 < numeroCidades; cidade1++)
            {
                for (int cidade2 = 0; cidade2 < numeroCidades; cidade2++)
                {
                    distancias[cidade1, cidade2] = uint.Parse(analisadorDistancias.Record[cidade2]);
                }
                analisadorDistancias.Read();
            }
        }

        string caminhoTrajeto = Path.Combine(caminhoDesktop, "caminho.txt");
        if (!File.Exists(caminhoTrajeto))
        {
            Console.WriteLine("Arquivo de trajeto não encontrado.");
            return;
        }

        uint tamanhoTrajeto;
        uint[] trajeto;
        using (StreamReader leitorTrajeto = new StreamReader(caminhoTrajeto))
        using (CsvParser analisadorTrajeto = new CsvParser(leitorTrajeto, configuracaoCsv))
        {
            if (!analisadorTrajeto.Read()) { return; }
            tamanhoTrajeto = (uint)(analisadorTrajeto.Record.Length);
            trajeto = new uint[tamanhoTrajeto];
            for (int parada = 0; parada < tamanhoTrajeto; parada++)
            {
                trajeto[parada] = uint.Parse(analisadorTrajeto.Record[parada]) - 1;
            }
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
