#define TERSE
#define VERBOSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Exam.Seventy_483.Csl.Chapter2
{
    class MySerializable
    {

        [Conditional("VERBOSE"), Conditional("TERSE")]
        static void reportHeader()
        {
            Console.WriteLine("This is the header for the report");
        }

        [Conditional("VERBOSE")]
        static void verboseReport()
        {
            Console.WriteLine("This is output from the verbose report.");
        }

        [Conditional("TERSE")]
        static void terseReport()
        {
            Console.WriteLine("This is output from the terse report.");
        }

        public static void TestCondition()
        {
            reportHeader();
            terseReport();
            verboseReport();

            if (Attribute.IsDefined(typeof(Person), typeof(SerializableAttribute))) 
                Console.WriteLine("Person can be serialized");

            /* The data values stored in an attribute instances are set from the class 
             * metadata when the attribute is loaded. A program can change them as it runs,
                but these changes will be lost when the program ends */

            Person person = new Person("Podge", 50);
            Console.WriteLine(person.Name);

        }

        public static void TestAttribute()
        {
            Attribute a = Attribute.GetCustomAttribute(typeof(Person), typeof(ProgrammerAttribute));

            ProgrammerAttribute p = (ProgrammerAttribute)a;

            Console.WriteLine("Programmer: {0}", p.Programmer);
        }
    }

    [Serializable]
    [ProgrammerAttribute(programmer: "Fred")]
    public class Person
    {
        
        public string Name { get; set; }

        public int Age;

        [NonSerialized]
        // No need to save this
        private int screenPosition;
        public Person()
        {

        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            screenPosition = 0;
        }
    }
    class ProgrammerAttribute : Attribute
    {
        private string programmerValue;

        public ProgrammerAttribute(string programmer)
        {
            programmerValue = programmer;
        }

        public string Programmer
        {
            get
            {
                return programmerValue;
            }
        }
    }

}
