using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class MyFlow
    {
        static int mOne()
        {
            Console.WriteLine("mOne called");
            return 1;
        }

        static int mTwo()
        {
            Console.WriteLine("mTwo called");
            return 2;
        }

        public static void TestLogicalExpressions()
        {
            if (mOne() == 1 & mTwo() == 2)
                Console.WriteLine("Hello world");

        }

        public static void TestLogicalExpressionsShortCircuit()
        {
            if (mOne() == 2 & mTwo() == 1)  // This calls both methods
                Console.WriteLine("Hello world");

            if (mOne() == 2 && mTwo() == 1)  // This calls the first method only, as false is returned = ShortCircuited
                Console.WriteLine("Hello world");

        }

        public static void EvaluateExpressions()
        {
            int i = 0; // create i and set to 0

            // Monadic operators - one operand 
            i++; // monadic ++ operator increment - i now 1 
            i--; // monadic -- operator decrement - i now 0

            // Postfix monadic operator - perform after value given
            Console.WriteLine(i++); // writes 0 and sets i to 1
            // Prefix monadic operator - perform before value given
            Console.WriteLine(++i); // writes 2 and sets i to 2 
        }

        public static void TestForLoops()
        {
            string[] names = { "Rob", "Mary", "David", "Jenny", "Chris", "Imogen" };

            for (int index = 0; index < names.Length; index++)
            {
                Console.WriteLine(names[index]);
            }
        }

        static int counter;
        static void Initalize()
        {
            Console.WriteLine("Initialize called");
            counter = 0;
        }

        static void Update()
        {
            Console.WriteLine("Update called");
            counter = counter + 1;
        }

        static bool Test()
        {
            Console.WriteLine("Test called");
            return counter < 5;
        }


        public static void TestForLoopConstuction()
        {
            /* 1. Initialization that is performed to set the loop up
               2. A test that will determine if the loop should continue
               3. An update to be performed each time the action of the loop has been performed
                */
            for (Initalize(); Test(); Update())
            {
                Console.WriteLine("Hello {0}", counter);
            }
        }

        public static void TestDoWhile()
        {
            /* A while construction is useful in situations where you want to repeat something as long
                as a condition is true. A do-while construction is useful in situations where you want to 
                do something and then repeat it if the action failed. */
            int count = 0;
            while (count < 10)
            {
                Console.WriteLine("Hello {0}", count);
                count = count + 1;
            }

            do
            {
                Console.WriteLine("Hello");
            } while (false);

        }

    }
}
