using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    public class MyConsumeType
    {

        public static void TestBoxing()
        {
            // the value 99 is boxed into an object
            object o = 99;

            // the boxed object is unboxed back into an int   
            //  The process of converting from a reference type(reference o) into 
            // a value type(the integer oVal) is  called unboxing. 
            int oVal = (int)o;
            Console.WriteLine(oVal);


            float x = 9.9f;
            // int i = x; Narrowing, data is lost
            int i = (int)x;  // Explict conversion 
            double dl = (double)oVal; // Widening

            // Casting cannot be used to convert between different types, for example with an integer and string
            // int ii = (int)"99"; 
        }

        public static void TestConversion()
        {
            Miles m = new Miles(100);

            Kilometers k = m; // implicity convert miles to km 
            Console.WriteLine("Kilometers: {0}", k.Distance);

            int intMiles = (int)m;  // explicitly convert miles to int
            Console.WriteLine("Int miles: {0}", intMiles);
        }

        public static void TestDynamic()
        {
            dynamic d = 99;
            d = d + 1;
            Console.WriteLine(d);

            d = "Hello";
            d = d + " Rob";
            Console.WriteLine(d);

            dynamic person = new ExpandoObject();

            person.Name = "Rob Miles";
            person.Age = 21;

            Console.WriteLine("Name: {0} Age: {1}", person.Name, person.Age);
        }
    }

    class Miles
    {
        public double Distance { get; }

        // Conversion operator for implicit converstion to Kilometers
        public static implicit operator Kilometers(Miles t)
        {
            Console.WriteLine("Implicit conversion from miles to kilometers");
            return new Kilometers(t.Distance * 1.6);
        }

        public static explicit operator int(Miles t)
        {
            Console.WriteLine("Explicit conversion from miles to int");
            return (int)(t.Distance + 0.5);
        }

        public Miles(double miles)
        {
            Distance = miles;
        }
    }

    class Kilometers
    {
        public double Distance { get; }

        public Kilometers(double kilometers)
        {
            Distance = kilometers;
        }
    }

 
}
