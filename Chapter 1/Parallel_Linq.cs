using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter1
{
    public class Parallel_Linq
    {
        private static Person[] people = new Person[] {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
        };

        public static void AsParallel()
        {
            var result = from person in people.AsParallel()
                         where person.City == "Seattle"
                         select person;

            foreach (var person in result)
                Console.WriteLine(person.Name);

            Console.WriteLine("\nForced");
            var result2 = from person in people.AsParallel().
                WithDegreeOfParallelism(4).
                WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                         where person.City == "Seattle"
                         select person;

            foreach (var person in result2)
                Console.WriteLine(person.Name);

            Console.WriteLine("\nOrdered, This can slow down the query");
            var result3 = from person in
                people.AsParallel().AsOrdered()
                         where person.City == "Seattle"
                        select person;

            foreach (var person in result3)
                Console.WriteLine(person.Name);

            Console.WriteLine("\nSequential,  ordering is preserved by the use of AsSequential before the Take ");

           var result4 = (from person in people.AsParallel()
                          where person.City == "Seattle"
                          orderby (person.Name)
                          select new
                          {
                              Name = person.Name
                          }).AsSequential().Take(3);

            foreach (var person in result4)
                Console.WriteLine(person.Name);

        }

        public static void ForAll()
        {
            /*The parallel nature of the execution of ForAll means that the order of the printed output
            above will not reflect the ordering of the input data. */

            var result = from person in
                          people.AsParallel()
                         where person.City == "Seattle"
                         select person;
            result.ForAll(person => Console.WriteLine(person.Name));
        }

        public static void GetException()
        {
            try
            {
                var result = from person in
                    people.AsParallel()
                             where CheckCity(person.City)
                             select person;
                result.ForAll(person => Console.WriteLine(person.Name));
            }
            catch (AggregateException e)
            {
                /* Note that the outer catch of AggregateException does catch any exceptions thrown by the 
                    CheckCity method. If elements of a query can generate exceptions it is considered good programming
                    practice to catch and deal with them as close to the source as possible. */

                Console.WriteLine(e.InnerExceptions.Count + " exceptions.");
            }
        }

        public static bool CheckCity(string name)
        {
            if (name == "")
                throw new ArgumentException(name);
            return name == "Seattle";
        }


    }

    class Person
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
