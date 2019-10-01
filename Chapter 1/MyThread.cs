using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public static class MyThread
    {
        static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        public static void ThreadStart()
        {
            Thread thread = new Thread(ThreadHello);
            thread.Start();
        }

        public static void ThreadStartDep()
        {
            // The ThreadStart delegate is no longer required.
            ThreadStart ts = new ThreadStart(ThreadHello);
            Thread thread = new Thread(ts);
            thread.Start();
        }

        public static void ThreadLamda()
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Hello from the thread");
                Thread.Sleep(1000);
            });
        }

        public static void ThreadData()
        {
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            Thread thread = new Thread(ps);
            thread.Start(99);
        }

        static void WorkOnData(object data)
        {
            Console.WriteLine("Working on: {0}", data);
            Thread.Sleep(1000);
        }

        public static void ThreadAbort()
        {
            Thread tickThread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            tickThread.Start();

            Console.WriteLine("Press a key to stop the clock");
            Console.ReadKey();
            tickThread.Abort();
        }

        static bool tickRunning;  // flag variable  
        public static void ThreadAbortWithFlag()
        {
            tickRunning = true;

            Thread tickThread = new Thread(() =>
            {
                while (tickRunning)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            tickThread.Start();

            Console.WriteLine("Press a key to stop the clock");
            Console.ReadKey();
            tickRunning = false;
        }

        public static void ThreadJoin()
        {
            Thread threadToWaitFor = new Thread(() =>
            {
                Console.WriteLine("Thread starting");
                Thread.Sleep(2000);
                Console.WriteLine("Thread done");
            });

            threadToWaitFor.Start();
            Console.WriteLine("Joining thread");
            threadToWaitFor.Join();
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }

        public static ThreadLocal<Random> RandomGenerator =
            new ThreadLocal<Random>(() =>
            {
                return new Random(2);
            });

        public static void ThreadLocalRun()
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t1: {0}", RandomGenerator.Value.Next(10));
                    Thread.Sleep(500);
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t2: {0}", RandomGenerator.Value.Next(10));
                }
                Thread.Sleep(500);

            });

            t1.Start();
            t2.Start();
        }

        static void DisplayThread(Thread t)
        {
            Console.WriteLine("Name: {0}", t.Name);
            Console.WriteLine("Culture: {0}", t.CurrentCulture);
            Console.WriteLine("Priority: {0}", t.Priority);
            Console.WriteLine("Context: {0}", t.ExecutionContext);
            Console.WriteLine("IsBackground?: {0}", t.IsBackground);
            Console.WriteLine("IsPool?: {0}", t.IsThreadPoolThread);
        }

        public static void ThreadContext()
        {
            Thread.CurrentThread.Name = "Main method";
            DisplayThread(Thread.CurrentThread);
        }

        static void DoWork(object state)
        {
            Console.WriteLine("Doing work: {0}", state);
            Thread.Sleep(500);
            Console.WriteLine("Work finished: {0}", state);
        }

        public static void ThreadPoolRun()
        {
            // Threads, like everything else in C#, are managed as objects.
            for (int i = 0; i < 50; i++)
            {
                int stateNumber = i;
                // The item of work is supplied as a WaitCallback delegate
                ThreadPool.QueueUserWorkItem(state => DoWork(stateNumber));
            }
        }

        /* if you create a large number of threads that may be idle for a very long time, this may
            block the ThreadPool, because the ThreadPool only contains a finite number of threads. 
  
            You cannot manage the priority of threads in the ThreadPool.

            Threads in the ThreadPool have background priority. You cannot obtain a thread with
            foreground priority from the ThreadPool.
 
            Local state variables are not cleared when a ThreadPool thread is reused. They therefore
            should not be used. 
        */

        private static double computeAverages(long noOfValues)
        {
            double total = 0;
            Random rand = new Random();

            for (long values = 0; values < noOfValues; values++)
            {
                total = total + rand.NextDouble();
            }

            return total / noOfValues;
        }

        public static void RunComputeAverages(long noOfValues)
        {
            Console.WriteLine(String.Format("Result: {0}", computeAverages(noOfValues)));
        }

        // A difficulty with tasks is that they can be hard for the programmer to manage. 

        /* The async and await keywords allow programmers to write code elements that execute
        asynchronously. The async keyword is used to flag a method as “asynchronous.” An asynchronous
        method must contain one or more actions that are “awaited.”
        */

        private static Task<double> asyncComputeAverages(long noOfValues)
        {
            return Task<double>.Run(() =>
            {
                return computeAverages(noOfValues);
            });
        }

        private static async Task<string> FetchWebPage(string url)
        {
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

        static async Task<IEnumerable<string>> FetchWebPages(string[] urls)
        {
            var tasks = new List<Task<String>>();

            foreach (string url in urls)
            {
                tasks.Add(FetchWebPage(url));
            }

            // The Task.WhenAll method is given a list of tasks and returns a collection 
            // which contains their results when they have completed.
            return await Task.WhenAll(tasks);
        }

        /* Thread safe collections:
         * BlockingCollection<T>
            ConcurrentQueue<T>
            ConcurrentStack<T>
            ConcurrentBag<T>
            ConcurrentDictionary<TKey, TValue>
         */

        public static void BlockingCollection()
        {
            // Blocking collection that can hold 5 items
            BlockingCollection<int> data = new BlockingCollection<int>(5);

            Task.Run(() =>
            {
                    // attempt to add 10 items to the collection - blocks after 5th
                    for (int i = 0; i < 11; i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added sucessfully.", i);
                }
                    // indicate we have no more to add
                    data.CompleteAdding();
            });

            Console.ReadKey();
            Console.WriteLine("Reading collection");

            Task.Run(() =>
            {
                while (!data.IsCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} taken sucessfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });
        }

        public static void ConcurrentQueue()
        {
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
            queue.Enqueue("Rob");
            queue.Enqueue("Miles");
            string str;
            if (queue.TryPeek(out str))
                Console.WriteLine("Peek: {0}", str);
            if (queue.TryDequeue(out str))
                Console.WriteLine("Dequeue: {0}", str);
        }

        public static void ConcurrentStack()
        {
            ConcurrentStack<string> stack = new ConcurrentStack<string>();
            stack.Push("Rob");
            stack.Push("Miles");
            string str;
            if (stack.TryPeek(out str))
                Console.WriteLine("Peek: {0}", str);
            if (stack.TryPop(out str))
                Console.WriteLine("Pop: {0}", str);
            Console.ReadKey();
        }

        public static void ConcurrentBag()
        {
            ConcurrentBag<string> bag = new ConcurrentBag<string>();
            bag.Add("Rob");
            bag.Add("Miles");
            bag.Add("Hull");
            string str;
            if (bag.TryPeek(out str))
                Console.WriteLine("Peek: {0}", str);
            if (bag.TryTake(out str))
                Console.WriteLine("Take: {0}", str);
        }

        public static void ConcurrentDictionary()
        {
            ConcurrentDictionary<string, int> ages = new ConcurrentDictionary<string, int>();
            if (ages.TryAdd("Rob", 21))
                Console.WriteLine("Rob added successfully.");
            Console.WriteLine("Rob's age: {0}", ages["Rob"]);
            // Set Rob's age to 22 if it is 21
            if (ages.TryUpdate("Rob", 22, 21))
                Console.WriteLine("Age updated successfully");
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
            // Increment Rob's age atomically using factory method
            Console.WriteLine("Rob's age updated to: {0}",
                ages.AddOrUpdate("Rob", 1, (name, age) => age = age + 1));
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
        }

        static object lock1 = new object();
        static object lock2 = new object();

        static void Method1()
        {
            lock (lock1)
            {
                Console.WriteLine("Method 1 got lock 1");
                Console.WriteLine("Method 1 waiting for lock 2");
                lock (lock2)
                {
                    Console.WriteLine("Method 1 got lock 2");
                }
                Console.WriteLine("Method 1 released lock 2");
            }
            Console.WriteLine("Method 1 released lock 1");
        }

        static void Method2()
        {
            lock (lock2)
            {
                Console.WriteLine("Method 2 got lock 2");
                Console.WriteLine("Method 2 waiting for lock 1");
                lock (lock1)
                {
                    Console.WriteLine("Method 2 got lock 1");
                }
                Console.WriteLine("Method 2 released lock 1");
            }
            Console.WriteLine("Method 2 released lock 2");
        }

        public static void SequentialLock()
        {
            Method1();
            Method2();
            Console.WriteLine("Methods complete. Press any key to exit.");
            Console.ReadKey();
        }

        /* The volatile keyword can only be applied to fields of a class or struct. Local variables cannot be declared volatile. */
        volatile static  int x;

        /* Operations involving the variable x will now not be optimized, and the value of x will be
            fetched from the copy in memory, rather than being cached in the processor. This can make
            operations involving the variable x a lot less efficient. */
        public static void Volatile()
        {
            int y = 0;
            x = 99;
            y = y + 1;
            Console.WriteLine("The answer is: {0}", x);
        }
    }
}
