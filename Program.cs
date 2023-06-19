using System;
using System.Collections.Generic;
using System.Globalization;

namespace FujtajblBeta
{

    // rozhraní hlavní metody celého programu
    public interface IOperation
    {
        string ProvedOperaci(decimal a, decimal b);
    }

    public class Scitani : IOperation
    {
        public string ProvedOperaci(decimal a, decimal b)
        {
            return "Vysledek je = " + (a + b);
        }
    }

    public class Odcitani : IOperation
    {
        public string ProvedOperaci(decimal a, decimal b)
        {
            return "Vysledek je = " + (a - b);
        }
    }

    public class Nasobeni : IOperation
    {
        public string ProvedOperaci(decimal a, decimal b)
        {
            return "Vysledek je = " + (a * b);
        }
    }

    public class Deleni : IOperation
    {
        public string ProvedOperaci(decimal a, decimal b)
        {
            return "Vysledek je = " + (a / b);
        }
    }

    //do budoucna možno přidávat různé jiné aritmetické operace ...


    //context pro volení strategie
    public class VoleniStrategie
    {
        private Dictionary<string, IOperation> operations;

        public VoleniStrategie()
        {
            operations = new Dictionary<string, IOperation>
            {
                { "1", new Scitani() },
                { "2", new Odcitani() },
                { "3", new Nasobeni() },
                { "4", new Deleni() }
            };
        }

        public IOperation ZvolOperaci(string operace)
        {
            if (operations.TryGetValue(operace, out var vypocet))
            {
                return vypocet;
            }
            else
            {
                return null; // or throw an exception indicating invalid vypocet
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            VoleniStrategie voleniStrategie = new VoleniStrategie();

            // while loop nám do budoucna umožní provádět vícero operací naráz bez nutnosti restartování programu

            while (true)
            {
                Console.WriteLine("Zadejte cislo A");
                decimal a;
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out a))
                {
                    Console.WriteLine("Neplatný vstup pro číslo A");
                    continue;
                }

                Console.WriteLine("Zadejte cislo B");
                decimal b;
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out b))
                {
                    Console.WriteLine("Neplatný vstup pro číslo B");
                    continue;
                }

                Console.Write(Environment.NewLine);
                Console.Write("Vyberte operaci" + Environment.NewLine);
                Console.Write("1: a + b" + Environment.NewLine);
                Console.Write("2: a - b" + Environment.NewLine);
                Console.Write("3: a * b" + Environment.NewLine);
                Console.Write("4: a / b" + Environment.NewLine);

                string operace = Console.ReadLine();

                IOperation vypocet = voleniStrategie.ZvolOperaci(operace);


                //zjisti zda se jedná o platnou či neplatnou operaci
                if (vypocet != null)
                {
                    string vysledek = vypocet.ProvedOperaci(a, b);
                    Console.WriteLine(vysledek);
                }
                else
                {
                    Console.WriteLine("Neznama operace!");
                }

                Console.WriteLine("Chcete spustit novy vypocet? a - ano; libovolná klávesa - exit");
                if (Console.ReadLine() != "a")
                {
                    break;
                }
            }
        }
    }
}




