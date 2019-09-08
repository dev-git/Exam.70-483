using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class Task
    {
        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        public static void CreateTask()
        {
            System.Threading.Tasks.Task newTask = new System.Threading.Tasks.Task(() => DoWork());
            newTask.Start();
            newTask.Wait();
        }

    }
}
