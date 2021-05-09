using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ExpleoTestApp
{
    public static class Expleo
    {
        static void Main(string[] args)
        {
            Selenium.WebScraper();
            Console.WriteLine("Press 'C' to clear console.");
            
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.KeyChar == 'c' || k.KeyChar == 'C')
            {
                Console.Clear();
            }

            for (int i = 1; i <= 3; i++)
            {
                string s = "";
                double d = 0;

                Console.WriteLine("Skriv ett mattematiskt uttryck, giltiga operatorer är \"+ - / *\"");
                Console.WriteLine($"Level:{i}");
                switch (i)
                {
                    case 1:
                        (d,s) = Calculator.EvaluteExpressionLevelOne(Console.ReadLine());
                        break;
                    case 2:
                        (d,s) = Calculator.EvaluteExpressionLevelTwo(Console.ReadLine());
                        break;
                    case 3:
                        (d,s) = Calculator.EvaluteExpressionLevelThree(Console.ReadLine());
                        break;
                }
                Console.WriteLine($"\n{s} = {d}");
            }
        }

    }
}
