using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    class MyInterface
    {
        public static void TestDisposable()
        {
            using (CrucialConnection c = new CrucialConnection())
            {
                // do something with the crucial connection
            }
        }
    }

    class CrucialConnection : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose called");
        }

    }

     /*  The IUnknown interface provides a means by which .NET
           applications can interoperate with COM objects at this level */
}
