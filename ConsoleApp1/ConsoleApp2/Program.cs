using System;

namespace ConsoleApp2
{
    class Program
    {
        private static int a = b;
        private static int b = 1;
        private static int c = b + 2;

        static void Main(string[] args)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
        }
    }
}
