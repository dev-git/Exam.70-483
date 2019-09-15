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
            //Chapter1.Parallel_Invoke.Invoke();

            // Chapter 1.3
            //Chapter1.Parallel_Fors.Parallel_ForEach();


            // Chapter 1.3
            //Chapter1.Parallel_Fors.Parallel_For();

            //Chapter1.Parallel_Fors.Parallel_Loop();

            //Chapter1.Parallel_Linq.AsParallel();

            //Chapter1.Parallel_Linq.ForAll();
            // Chapter1.Parallel_Linq.GetException();

            //Chapter1.MyTask.CreateTask();
            //Chapter1.MyTask.CreateTaskNew();
            //Chapter1.MyTask.GetResultFromTask();
            //Chapter1.MyTask.TaskWait();
            //Chapter1.MyTask.ContinationTask();
            //Chapter1.MyTask.ContinuationTaskWithOptions();
            //Chapter1.MyTask.ChildTasks();

            //Chapter1.MyThread.ThreadStart();
            //Chapter1.MyThread.ThreadData();
            //Chapter1.MyThread.ThreadAbort();
            //Chapter1.MyThread.ThreadAbortWithFlag();
            Chapter1.MyThread.ThreadJoin();


            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();


        }
    }
}
