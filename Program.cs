using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Chapter 1.1
            //Chapter1.Parallel.Invoke(() => Parrallel_Invoke.Task1(), () => Parrallel_Invoke.Task2());

            // Chapter 1.3
            //Chapter1.Parallel_Fors.Parallel_ForEach();


            // Chapter 1.3
            //Chapter1.Parallel_Fors.Parallel_For();

            //Chapter1.Parallel_Fors.Parallel_Loop();

            //Chapter1.Parallel_Linq.AsParallel();

            //Chapter1.Parallel_Linq.ForAll();
            Chapter1.Parallel_Linq.GetException();


            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();


        }
    }
}
