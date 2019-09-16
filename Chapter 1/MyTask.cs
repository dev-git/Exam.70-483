using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class MyTask
    {
        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        public static void DoWork(int i)
        {
            Console.WriteLine("Task {0} starting", i);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finished", i);
        }

        public static void CreateTask()
        {
            System.Threading.Tasks.Task newTask = new System.Threading.Tasks.Task(() => DoWork());
            newTask.Start();
            newTask.Wait();
        }

        public static void CreateTaskNew()
        {
            System.Threading.Tasks.Task newTask = System.Threading.Tasks.Task.Run(() => DoWork());
            newTask.Wait();
        }

        public static void GetResultFromTask()
        {
            Task<int> task = System.Threading.Tasks.Task.Run(() =>
            {
                return CalculateResult();
            });

            Console.WriteLine(task.Result);

        }

        public static void TaskWait()
        {
            System.Threading.Tasks.Task[] Tasks = new System.Threading.Tasks.Task[10];

            for (int i = 0; i < 10; i++)
            {
                int taskNum = i;  // make a local copy of the loop counter so that the 
                                  // correct task number is passed into the  lambda expression
                Tasks[i] = System.Threading.Tasks.Task.Run(() => DoWork(taskNum));

            }

            /* WaitAll will pause until all the horses have finished running, 
             * whereas WaitAny will pause until the first horse has finished running */
            System.Threading.Tasks.Task.WaitAll(Tasks);
        }

        public static void ContinationTask()
        {
            Task task = Task.Run(() => HelloTask());
            task.ContinueWith((prevTask) => WorldTask());

        }

        public static void ContinuationTaskWithOptions()
        {
            Task task = Task.Run(() => HelloTask());

            task.ContinueWith((prevTask) => WorldTask(), TaskContinuationOptions.
                                            OnlyOnRanToCompletion);
            task.ContinueWith((prevTask) => ExceptionTask(), TaskContinuationOptions.OnlyOnFaulted);
        }


        public static void ChildTasks()
        {
            /* Note that tasks created using the Task.Run method have the TaskCreationOptions.DenyChildAttach option set, 
             * and therefore can’t have attached child tasks */
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent starts");
                for (int i = 0; i < 10; i++)
                {
                    int taskNo = i;
                    Task.Factory.StartNew(
                        (x) => DoChild(x), // lambda expression
                               taskNo, // state object
                               TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait(); // will wait for all the attached children to complete
        }

        public static int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }

        public static void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }

        public static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }


        public static void ExceptionTask()
        {
            Thread.Sleep(500);
            Console.WriteLine("Error");
        }

        public static void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting", state);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished", state);
        }

        // make an array that holds the values 0 to 50000000
        private static int[] items = Enumerable.Range(0, 50000001).ToArray();
        public static void TaskSumming()
        {

            long total = 0;

            for (int i = 0; i < items.Length; i++)
                total = total + items[i];

            Console.WriteLine("The total is: {0}", total);

        }

        private static object sharedTotalLock = new object();
        private static long sharedTotal;

        static void addRangeOfValues(int start, int end)
        {
            //while (start < end)
            //{
            //    sharedTotal = sharedTotal + items[start]; start++;
            //}

            //return;
            while (start < end)
            {
                lock (sharedTotalLock)
                {
                    sharedTotal = sharedTotal + items[start];
                }
                start++;
            }
        }


        static void addRangeOfValuesSensible(int start, int end)
        {
            long subTotal = 0;

            while (start < end)
            {
                subTotal = subTotal + items[start];
                start++;
            }
            lock (sharedTotalLock)
            {
                sharedTotal = sharedTotal + subTotal;
            }
        }

        public static void BadTask()
        {
            List<Task> tasks = new List<Task>();

            int rangeSize = 1000;
            int rangeStart = 0;

            while (rangeStart < items.Length)
            {
                int rangeEnd = rangeStart + rangeSize;

                if (rangeEnd > items.Length)
                    rangeEnd = items.Length;

                // create local copies of the parameters
                int rs = rangeStart;
                int re = rangeEnd;

                //tasks.Add(Task.Run(() => addRangeOfValuesSensible(rs, re)));
                tasks.Add(Task.Run(() => addRangeOfValuesMonitored(rs, re)));
                rangeStart = rangeEnd;

            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("The total is: {0}", sharedTotal);

        }

        static void addRangeOfValuesMonitored(int start, int end)
        {
            long subTotal = 0;

            while (start < end)
            {
                subTotal = subTotal + items[start];
                start++;
            }

            Monitor.Enter(sharedTotalLock);
            sharedTotal = sharedTotal + subTotal;
            Monitor.Exit(sharedTotalLock);
        }
    }
}
