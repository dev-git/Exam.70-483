using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    using ExtensionMethods;

    public class MyType
    {
        struct StructStore
        {
            /* The constructor for a structure must initialize all the data members in the structure. 
             * Data members cannot be initialized in the structure. */
            public int Data { get; set; }

            // It is not possible to create a structure by extending a parent structure object.
            // Structure instances are generally created on the program stack unless they are used in closures
        }

        class ClassStore
        {
            public int Data { get; set; }
        }

        public static void MainType(string[] args)
        {
            // Struct variables are manged by value
            // eg. Alien coordinates, single value not shared
            // value types are Enumerated types and structures
            StructStore xs, ys;
            ys = new StructStore();
            ys.Data = 99;
            xs = ys;
            xs.Data = 100;
            Console.WriteLine("xStruct: {0}", xs.Data);
            Console.WriteLine("yStruct: {0}", ys.Data);

            // Class variables are managed by reference
            // eg. Alien sound effects, shared 
            // Reference types are classes
            ClassStore xc, yc;
            yc = new ClassStore();
            yc.Data = 99;
            xc = yc;
            xc.Data = 100;
            Console.WriteLine("xClass: {0}", xc.Data);
            Console.WriteLine("yClass: {0}", yc.Data);
        }

        public static void Helper()
        {
            int x, y = 0;
            x = y;

            /* If the variables x and y are value types, the result of the first assignment is that the value of 
                y is copied into the variable x. */

            /* If the variables x and y are reference types, the result of the first assignment is that the
                variable x is made to refer to the same object as that referred to by variable y.  */

        }

        /* Memory to be used to store variables of value type is allocated on the stack. The stack is an
            area of memory that is allocated and removed as programs enter and leave blocks. Any value
            type variables created during the execution of a block are stored on a local stack frame and
            then the entire frame is discarded when the block completes. This is an extremely efficient way
            to manage memory. */
        struct Alien
        {
            public int X;
            public int Y;
            public int Lives;

            public Alien(int x, int y)
            {
                X = x;
                Y = y;
                Lives = 3;
            }

            public override string ToString()
            {
                // a structure cannot be used in a class hierarchy, because it is
                // possible to override methods from the parent type of struct.
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        enum AlienState
        {
            Sleeping,
            Attacking,
            Destroyed
        };

        /* Memory to be used to store variables of reference type is allocated on a different structure,
            called the heap. The heap is managed for an entire application. The heap is required
            because, as references may be passed between method calls as parameters, it is not the case
            that objects managed by reference can be discarded when a method exits. Objects can only
            be removed from the heap when the garbage collection process determines that there are no
            references to them. */
        class AlienClass
        {
            // the word static in this context means that the variable is always present
            // A program can use a static variable from a type without needing to have created any instances of that type
            public static int Max_Lives = 99;

            static AlienClass()
            {
                Console.WriteLine("Static Alien constructor running");
            }

            public int X;
            public int Y;
            public int Lives;

            public AlienClass(int x, int y)
            {
                X = x;
                Y = y;
                Lives = 3;
            }

            public bool RemoveLives(int livesToRemove)
            {
                Lives = Lives - livesToRemove;

                if (Lives <= 0)
                {
                    Lives = 0;
                    X = -1000;
                    Y = -1000;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public override string ToString()
            {
                return string.Format("X: {0} Y: {1} Lives: {2}", X, Y, Lives);
            }
        }

        public static void TestAlienStruct()
        {
            Alien a;
            a.X = 50;
            a.Y = 50;
            a.Lives = 3;
            Console.WriteLine("a {0}", a.ToString());

            Alien x = new Alien(100, 100);
            Console.WriteLine("x {0}", x.ToString());

            Alien[] swarm = new Alien[100];
            Console.WriteLine("swarm [0] {0}", swarm[0].ToString());

            AlienState x1 = AlienState.Attacking;
            Console.WriteLine(x1);
        }

        public static void TestAlienClass()
        {
            /* When the new keyword is used to create an instance of a class the following sequence is performed:
            1. The program code that implements the class is loaded into memory, if it is not already present. 
            2. If this is the first time that the class has been referenced, any static members of the class
                are initialized and the static constructor is called.
            3. The constructor in the class is called. */

            AlienClass x = new AlienClass(100, 100);
            Console.WriteLine("x {0}", x);

            AlienClass[] swarm = new AlienClass[100];

            for (int i = 0; i < swarm.Length; i++)
                swarm[i] = new AlienClass(0, 0);

            Console.WriteLine("swarm [0] {0}", swarm[0]);

        }

        /* Generic types are used extensively in C# collections, such as with the List and Dictionary
            classes. They allow you to create a List  of any type of data, or a Dictionary of any type,
             indexed on any type.  */
            class MyStack<T> where T : class
        {
            /*  where T : class The type T must be a reference type.
                where T : struct The type T must be a value type.
                where T : new() The type T must have a public, parameterless, constructor. Specify this 
                constraint last if you are specifying a list of constraints
                where T : <base class> The type T must be of type base class or derive from base class.
                where T : <interface name> The type T must be or implement the specified interface. You can specify 
                multiple interfaces.
                where T : unmanaged The type T must not be a reference type or contain any members which are 
                reference types. */
            int stackTop = 0;
            T[] items = new T[100];

            public void Push(T item)
            {
                if (stackTop == items.Length)
                    throw new Exception("Stack full");
                items[stackTop] = item;
                stackTop++;
            }

            public T Pop()
            {
                if (stackTop == 0)
                    throw new Exception("Stack empty");
                stackTop--;
                return items[stackTop];
            }
        }

        class MyStackGenericValueType<T> where T : struct
        {
            int stackTop = 0;
            T[] items = new T[100];

            public void Push(T item)
            {
                if (stackTop == items.Length)
                    throw new Exception("Stack full");
                items[stackTop] = item;
                stackTop++;
            }

            public T Pop()
            {
                if (stackTop == 0)
                    throw new Exception("Stack empty");
                stackTop--;
                return items[stackTop];
            }
        }

        public static void TestMyStack()
        {
            MyStack<string> nameStack = new MyStack<string>();
            nameStack.Push("Rob");
            nameStack.Push("Mary");
            Console.WriteLine(nameStack.Pop());
            Console.WriteLine(nameStack.Pop());
        }

        public static void TestGenericValueType()
        {
            MyStackGenericValueType<Alien> alienStack = new MyStackGenericValueType<Alien>();
            alienStack.Push(new Alien(10, 10));
            alienStack.Push(new Alien());
            Console.WriteLine(alienStack.Pop());
            Console.WriteLine(alienStack.Pop());
        }

        public static void TestExtension()
        {
            string text = @"A rocket explorer called Wright,
                Once travelled much faster than light,
                He set out one day,
                In a relative way,
                And returned on the previous night";

            Console.WriteLine(text.LineCount());

        }

        static int ReadValue(
            int low,    // lowest allowed value
            int high,    // highest allowed value
            string prompt = "hi" // prompt for the user 
            )
        {
            // method body...
            Console.WriteLine("Low {0}, High {1}, Prompt {2}", low, high, prompt);
            return 1;
        }

        public static void TestParameters()
        {
            // Named variables
            var x = ReadValue(low: 1, high: 100, prompt: "Enter your age: ");

            // Optional parameters
            var x2 = ReadValue(25, 100);


        }

        class IntArrayWrapper
        {
            // Create an array to store the values
            private int[] array = new int[100];

            // Declare an indexer property
            public int this[int i]
            {
                get { return array[i]; }
                set { array[i] = value; }
            }
        }

        public static void TestMyIntArray()
        {
            IntArrayWrapper x = new IntArrayWrapper();

            x[0] = 99;
            Console.WriteLine(x[0]);

            NamedIntArray xx = new NamedIntArray(); 
            xx["zero"] = 99;
            Console.WriteLine(xx["zero"]);

        }

        class NamedIntArray
        {
            // Create an array to store the values
            private int[] array = new int[100];

            // Declare an indexer property
            public int this[string name]
            {
                get
                {
                    switch (name)
                    {
                        case "zero":
                            return array[0];
                        case "one":
                            return array[1];
                        default:
                            return -1;
                    }
                }
                set
                {
                    switch (name)
                    {
                        case "zero":
                            array[0] = value;
                            break;
                        case "one":
                            array[1] = value;
                            break;
                    }
                }
            }
        }

        /* The underlying principle of a class hierarchy is that classes at the top of the hierarchy 
         * are more abstract, and classes toward the bottom of the hierarchy are more specific */

        class Document
        {
            // All documents have the same GetDate behavior so
            // this method will not be overridden
            public void GetDate()
            {
                Console.WriteLine("Hello from GetDate in Document");
            }

            // A document may have its own DoPrint behavior so
            // this method is virtual so it can be overriden
            public virtual void DoPrint()
            {
                Console.WriteLine("Hello from DoPrint in Document");
            }
        }

        // The Invoice class derives from the Document class
        class Invoice : Document
        {
            // Override the DoPrint method in the base class
            // to provide custom printing behaviour for an Invoice
            public override void DoPrint()
            {
                base.DoPrint();
                Console.WriteLine("Hello from DoPrint in Invoice");
            }
        }


        public static void TestOverriding()
        {
            // Create an Invoice
            Invoice c = new Invoice();
            // This will run the SetDate method from Document
            c.GetDate();
            // This will run the DoPrint method from Invoice 
            c.DoPrint();
            

        }
    }
}
 
 
 
 
 
 
 
 