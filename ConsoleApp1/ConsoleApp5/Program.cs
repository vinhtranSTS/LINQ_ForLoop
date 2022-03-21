using System;

namespace ConsoleApp5
{
    class Program
    {
        public class D
        {
            public int Value { get; set; }
            public int i { get; set; }
            public void Cal()
            {
                 i = Value + 2;
            }
        }

        static void Main(string[] args)
        {
            var d = new D();
            d.Value = 0;
            d.Cal();
            Console.WriteLine("zzz " + d.i);
        }
    }
}
