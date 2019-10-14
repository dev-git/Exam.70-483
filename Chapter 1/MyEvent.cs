using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    class Alarm
    {
        // Delegate for the alarm event
        //  delegate. This is a piece of data that contains a reference to a particular method in a class
        public Action OnAlarmRaised { get; set; }
        /* this means that code external to the Alarm object can raise the alarm by directly calling 
         * the OnAlarmRaised delegate. External code can overwrite the value of OnAlarmRaised  potentially removing subscribers */

        // Called to raise an alarm
        public void RaiseAlarm()
        {
            // Only raise the alarm if someone has
            // subscribed. 
            if (OnAlarmRaised != null)
            {
                OnAlarmRaised();
            }
        }
    }

    /* The keyword event is added before the definition of the delegate. The
        member OnAlarmRaised  is now created as a data field in the Alarm  class, rather than a property. 
        OnAlarmRaised no longer has get or set behaviors */
    class EventBasedAlarm
    {
        class Alarm
        {
            // Delegate for the alarm event
            public event Action OnAlarmRaised = delegate { };
            // Called to raise an alarm
            public void RaiseAlarm()
            {
                OnAlarmRaised();
            }
        }
    }

    /* the EventHandler class is the part of .NET designed to allow subscribers to be given data about an event.  
     * EventHandler is used throughout the .NET framework to manage events. 
     * An EventHandler can deliver data, or it can just signal that an event has taken place
     * */
    class EventHandlerAlarm
    {
        /* The EventHandler delegate refers to a subscriber method that will accept two arguments.
            The first argument is a reference to the object raising the event. The second argument is a
            reference to an object of type EventArgs that provides information about the event. */
        // Delegate for the alarm event 
        public event EventHandler OnAlarmRaised = delegate { };

        // Called to raise an alarm
        // Does not provide any event arguments
        public void RaiseAlarm()
        {
            // Raises the alarm
            // The event handler receivers a reference to the alarm that is 
            // raising this event
            OnAlarmRaised(this, EventArgs.Empty);
        }
    }


    class AlarmEventArgs : EventArgs
    {
        public string Location { get; set; }

        public AlarmEventArgs(string location)
        {
            Location = location;
        }
    }

    class EventDataAlarm
    {

        // Delegate for the alarm event
        public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };
        // Called to raise an alarm
        public void RaiseAlarm(string location)
        {
            OnAlarmRaised(this, new AlarmEventArgs(location));
        }
    }

    class RaiseAlarmException
    {
        public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };

        public void RaiseAlarm(string location)
        {
            List<Exception> exceptionList = new List<Exception>();

            /* The word Delegate is the abstract class that defines the behavior of delegate instances. 
             * Once the delegate keyword has been used to create a delegate type, 
                 objects of that delegate type will be realized as Delegate instance */
            foreach (Delegate handler in OnAlarmRaised.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, new AlarmEventArgs(location));
                }
                catch (TargetInvocationException e)
                {
                    exceptionList.Add(e.InnerException);
                }
            }
            if (exceptionList.Count > 0)
                throw new AggregateException(exceptionList);
        }
    }
    public class MyEvent
    {
        // Method that must run when the alarm is raised        
        static void AlarmListener1()
        {
            Console.WriteLine("Alarm listener 1 called");
        }

        // Method that must run when the alarm is raised        
        static void AlarmListener2()
        {
            Console.WriteLine("Alarm listener 2 called");
        }

        private static void AlarmListener1(object sender, EventArgs e)
        {
            // Only the sender is valid as this event doesn't have arguments    
            Console.WriteLine("Alarm listener 1 no event called");
        }

        static void AlarmListener1(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm listener 1 called");
            Console.WriteLine("Alarm in {0}", args.Location);
        }

        static void AlarmListener1x(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm listener 1 called");
            Console.WriteLine("Alarm in {0}", args.Location);
            throw new Exception("Bang");
        }

        static void AlarmListener2(object source, AlarmEventArgs args)
        {
            Console.WriteLine("Alarm listener 2 called");
            Console.WriteLine("Alarm in {0}", args.Location);
            throw new Exception("Boom");
        }

        public static void CreateAlarm()
        {
            // Create a new alarm
            Alarm alarm = new Alarm();

            // Connect the two listener methods
            // Subscribers bind to a publisher by using the += operator
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            // raise the alarm
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");

        }

        public static void Unsubscribe()
        {  // Create a new alarm
            Alarm alarm = new Alarm();

            // Connect the two listener methods
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");

            alarm.OnAlarmRaised -= AlarmListener1;
            alarm.RaiseAlarm();
            Console.WriteLine("Alarm raised");
        }

        public static void TestEventHandler()
        {
            EventHandlerAlarm alarm = new EventHandlerAlarm();
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.RaiseAlarm();
            //AlarmListener1(this, e)

        }

        public static void TestEventDataHandler()
        {
            EventDataAlarm dataAlarm = new EventDataAlarm();

            // subscriber += listener
            dataAlarm.OnAlarmRaised += AlarmListener1;
            /* Note that a reference to the same AlarmEventArgs object is passed to each of the subscribers
                to the OnAlarmRaised event */
            dataAlarm.RaiseAlarm("Auckland");
        }

        public static void TestAlarmException()
        {
            RaiseAlarmException raiseAlarmException = new RaiseAlarmException();

            raiseAlarmException.OnAlarmRaised += AlarmListener1x;
            raiseAlarmException.OnAlarmRaised += AlarmListener2;
            try
            {
                //alarm.RaiseAlarm("Kitchen");
                raiseAlarmException.RaiseAlarm("Glen Eden");
            }
            catch (AggregateException agg)
            {
                foreach (Exception ex in agg.InnerExceptions)
                    Console.WriteLine(ex.Message);
            }

        }
    }

    class MyDelegate
    {
        // The word delegate is the keyword used in a C# program that tells the compiler to create a delegate type
        delegate int IntOperation(int a, int b);

        static int Add(int a, int b)
        {
            Console.WriteLine("A d d  c alle d");
            return a + b;
        }
        static int Subtract(int a, int b)
        {
            Console.WriteLine("S u b trac t  c alle d");
            return a - b;
        }

        public static void TestMyDelegate()
        {
            // A program can use the variable op to either hold a collection of subscribers or to refer to a single method.
            IntOperation op;

            // Explicitly create the delegate
            op = new IntOperation(Add);
            Console.WriteLine(op(2, 2));

            // Delegate is created automatically
            // from method
            op = Subtract;
            Console.WriteLine(op(2, 2));

        }

    }

    public class LamdaExpression
    {
        delegate int IntOperation(int a, int b);

        // The operator => is called the lambda operator.
        static IntOperation add = (a, b) => { return 1; };// a + b + 1;

        delegate int op(int i);

        static op square = i => i * i;
        private static Action myvar;

        public static void TestLamda()
        {
            Console.WriteLine("Calling add {0}", add(2, 2));
            Console.WriteLine("Calling square {0}", square(2));

            add = (a, b) =>
            {
                Console.WriteLine("Add called");
                return a + b;
            };

            Console.WriteLine("Calling add {0}", add(3, 3));
        }

        public static void TestTask()
        {
            /* A lambda expression used in this way can be described as an anonymous method; 
             * because it is a piece of functional code that doesn’t have a name. */
            Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(500);
                }
            });

            Console.WriteLine("Task running..");

        }

        //delegate void myvar;

        public static void MyTask()
        {
            myvar = () => { Console.WriteLine("hi"); };
            //myvar;
        }

    }

    public class LamdaClosure
    {
        delegate int GetValue();

        static GetValue getLocalInt;

        static void SetLocalInt()
        {
            // Local variable set to 99
            int localInt = 99;

            // Set delegate getLocalInt to a lambda expression that
            // returns the value of localInt
            getLocalInt = () => localInt;
            /* The compiler makes sure that the localInt variable is available for use in the lambda expression 
             * when it is subsequently called from the Main method.  This extension of variable life is called a closure. */
        }

        public static void TestClosure()
        {
            SetLocalInt();

            Console.WriteLine("Value of localInt {0}", getLocalInt());
        }

    }

    public class BuiltinDelegate
    {
        // The Func types provide a range of delegates for methods that accept values and return results
        static Func<int, int, int> add = (a, b) => a + b;
        
        // If the lambda expression doesn’t return a result, you can use the Action type that you saw
        // earlier when we created our first delegates.The statement below creates a delegate
        static Action<string> logMessage = (message) => Console.WriteLine(message);

        /* The Predicate built in delegate type lets you create code that takes a 
         * value of a particular type and returns true or false. */
        static Predicate<int> dividesByThree = (i) => i % 3 == 0;

        public static void Test()
        {
            Console.WriteLine("Add called for 2 + 2: {0}", add(2, 2));
            logMessage("Log message called");
            Console.WriteLine("Divide by three called for 9: {0}", dividesByThree(9));
            Console.WriteLine("Divide by three called for 10: {0}", dividesByThree(10));
        }
    }

}
