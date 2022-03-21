using System;

namespace ConsoleApp3
{
    class Program
    {
        public class D
        {
            public string Name { get; set; }

            public void SetName(D a, string name = "Tran")
            {
                a.Name = name; 
                a = new D { Name = "Vinh" };
            }
        }


        static void Main(string[] args)
        {
            var d = new D(); 
            d.SetName(d, "Quang");
            Console.WriteLine(d.Name);
        }
    }
}
