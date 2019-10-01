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

        public static void ThrowException()
        {
            try
            {
                throw new Exception("I think you should know that I'm feeling very depressed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // how to rethrow
                throw ex; // this will not preserve the original stack trace
            }
        }

        public static void InnerException()
        {
            int badno = 0;
            try
            {
                var myvar = 1 / badno;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // The divide by zero becomes the inner exception
                throw new Exception("Something bad happened", ex);
            }
        }

        class CalcException : Exception
        {
            public enum CalcErrorCodes
            {
                InvalidNumberText,
                DivideByZero
            }

            public CalcErrorCodes Error { get; set; }

            public CalcException(string message, CalcErrorCodes error) 
                : base(message)
            {
                Error = error;
            }
        }

        public static void TestCustomException()
        {
            try
            {
                throw new CalcException("Calc failed", CalcException.CalcErrorCodes.InvalidNumberText);
            }
            catch (CalcException ce)
            {
                Console.WriteLine("Error: {0}", ce.Error);
            }

        }

        public static void TestConditionalException()
        {
            /* This mechanism is more efficient than re-throwing an exception, because the .NET runtime
                doesn’t have to rebuild the exception object prior to re-throwing it. */
            try
            {
                throw new CalcException("Calc failed", CalcException.CalcErrorCodes.DivideByZero);
            }
            catch (CalcException cedvz) when (cedvz.Error == CalcException.CalcErrorCodes.DivideByZero)
            {
                Console.WriteLine("Divide by zero error");
            }
        }

        public static void HandleInnerException()
        {
            try
            {
                try
                {
                    Console.Write("Enter an integer: ");
                    string numberText = Console.ReadLine();
                    int result;
                    result = int.Parse(numberText);
                }
                catch (Exception ex)
                {
                    throw new Exception("Calculator failure", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);
            }
        }

    }
}
