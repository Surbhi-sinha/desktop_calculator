using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Expression

using System.Threading.Tasks;
using desktop_calculator;

namespace Desktop_calculator_testconsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExpressionCalculator calculator = new ExpressionCalculator();

            Console.WriteLine("\nexpression : 256+Tan(40-5)\n" + calculator.Evaluate("256+Tan(40-5)"));
            Console.WriteLine("\nexpression : -256+66+(64/2)-72\n" + calculator.Evaluate("-256+66+(64/2)-72"));
            Console.WriteLine("\nexpression : 256+Tan(4.5)-Sin(45)\n" + calculator.Evaluate("256+Tan(45)-Sin(45)"));
            Console.WriteLine("\nexpression : 2+(45)/(-3)\n" + calculator.Evaluate("2+(45)/(-3)"));
            Console.WriteLine("\nexpression : -2+(-0.3)*(-5)\n" + calculator.Evaluate("-2+(-0.3)*(-5)"));
            Console.WriteLine("\nexpression : -8*(-2)+(-2+2)-Log(1000)\n" + calculator.Evaluate("-8*(-2)+(-2+2)-Log(1000)"));
            Console.WriteLine("\nexpression : -Log(Sqrt(100))\n" + calculator.Evaluate("-Log(Sqrt(100))"));
            Console.WriteLine("\nexpression : 5.7*(-2.2))\n" + calculator.Evaluate("5.7*(-2.2)"));
            Console.WriteLine("\nexpression : Cos(2^2)\n" + calculator.Evaluate("Cos(2^2)"));

            GC.Collect();
            Console.ReadKey();

        }
    }
}
