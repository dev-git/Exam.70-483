using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class Parallel_Fors
    {
        public static void Parallel_ForEach()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();

        }

        public static void Parallel_For()
        {
            var items = Enumerable.Range(0, 500).ToArray();

            // i => { WorkOnItem(items[i]); } is a lamda expression
            Parallel.For(0, items.Length, i => { WorkOnItem(items[i]); });

        }

        public static void Parallel_Loop()
        {
            /* If Stop  is used to stop the loop during the 200th iteration it might be that iterations
                with an index lower than 200 will not be performed. If Break is used to end the loop iteration,
                all the iterations with an index lower than 200 are guaranteed to be completed before the loop
                is ended */

            var items = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, items.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 200)
                    loopState.Stop();
                WorkOnItem(items[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }



    }
}
