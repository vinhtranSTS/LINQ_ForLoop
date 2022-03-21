using System;

namespace ConsoleApp4
{
    class Program
    {
        public class A
        {
            public A()
            {
                Console.WriteLine(" A");
            }

            ~A()
            {
                Console.WriteLine("~A");
            }
        }


        public class B : A
        {
            public B()
            {
                Console.WriteLine(" B");
            }

            ~B()
            {
                Console.WriteLine("~B");
            }
        }

        public static void test()
        {
            var b = new B();
            b = null;
        }

        static void Main(string[] args)
        {

          //  test();
            var b = new B();
            b = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.ReadKey();
        }
    }
}
