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
            Parallel.Invoke(() => Parrallel_Invoke.Task1(), () => Parrallel_Invoke.Task2());
            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();


        }
    }
}
