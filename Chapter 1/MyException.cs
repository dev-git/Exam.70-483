using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class MyException
    {
        public static void TestException()
        {
            try
            {
                Console.Write("Enter an integer: ");
                string numberText = Console.ReadLine();
                int result;
                result = int.Parse(numberText);
                Console.WriteLine("You entered {0}", result);
                int sum = 1 / result;
                Console.WriteLine("Sum is {0}", sum);
            }
            catch (NotFiniteNumberException nx)
            {
                Console.WriteLine("Invalid number");
            }
            catch (DivideByZeroException zx)
            {
                Console.WriteLine("Divide by zero");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception");
                /* “A previous catch clause already catches all exceptions of this or of a super type (‘Exception’)”. 
                    You must put the most abstract exception type last in the sequence. */
            }
            finally
            {
                Console.WriteLine("Thanks for using my program.");
                /* The only situation in which a finally block will not be executed are:
                    If preceding code (in either the try block or an exception handler) enters an infinite loop.
                    If the programmer uses the Environment.FailFast method in the code protected by the 
                    try construction to explicitly request that any finally elements are ignored. */
            }

        }
    }
}
