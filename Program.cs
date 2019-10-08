using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl
{
    using ExtensionMethods;

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
            //Chapter1.MyThread.ThreadJoin();
            //Chapter1.MyThread.ThreadLocalRun();
            //Chapter1.MyThread.ThreadContext().
            //Chapter1.MyThread.ThreadPoolRun();
            //Chapter1.MyThread.RunComputeAverages(10000000);
            //Chapter1.MyThread.BlockingCollection();
            //Chapter1.MyThread.ConcurrentStack();
            //Chapter1.MyThread.ConcurrentBag();
            //Chapter1.MyThread.ConcurrentDictionary();

            //Chapter1.MyTask.TaskSumming();
            //Chapter1.MyTask.BadTask();

            //Chapter1.MyThread.SequentialLock();
            //Chapter1.MyThread.Volatile();
            //Chapter1.MyTask.CancelTask();
            //Chapter1.MyTask.CancelWithException();

            //Chapter1.MyEvent.CreateAlarm();
            //Chapter1.MyEvent.Unsubscribe();
            //Chapter1.MyEvent.TestEventHandler();
            //Chapter1.MyEvent.TestEventDataHandler();

            //Chapter1.MyEvent.TestAlarmException();
            //Chapter1.MyDelegate.TestMyDelegate();
            //Chapter1.LamdaExpression.TestLamda();
            //Chapter1.LamdaClosure.TestClosure();
            //Chapter1.BuiltinDelegate.Test();
            //Chapter1.LamdaExpression.TestTask();    
            //Chapter1.LamdaExpression.MyTask();
            //Chapter1.MyFlow.TestLogicalExpressionsShortCircuit();

            //Chapter1.MyException.InnerException();
            //Chapter1.MyException.TestCustomException();
            //Chapter1.MyException.TestConditionalException();
            //Chapter1.MyException.HandleInnerException();



            //Chapter2.MyType.MainType(null);
            //Chapter2.MyType.TestAlienStruct();
            //Chapter2.MyType.TestAlienClass();

            //Chapter2.MyType.TestMyStack();
            //Chapter2.MyType.TestGenericValueType();
            //Chapter2.MyType.TestParameters();
            //Chapter2.MyType.TestOverriding();

            Chapter2.MyAccount.TestBabyAccount();


            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();


        }
    }
}
