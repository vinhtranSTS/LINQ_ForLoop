using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> abc = new List<string> { "1", "2", "3" };
            var index = new List<int>();
            for (int i = 0; i < abc.Count; i++)
            {
                if (abc[i] == "2")
                {
                    Console.WriteLine(abc[i]);
                    abc.Remove(abc[i]);


                    index.Add(i);
                }
            }

            for (int i = 0; i < index.Count; i++)
            {
                abc.RemoveAt(index[i]);
            }


            for (int i = 0; i < abc.Count; i++)
            {
                Console.WriteLine("zzz "+abc[i]);
            }
        }
    }
}
