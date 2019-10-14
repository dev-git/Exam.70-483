using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    class MyReflection
    {
        public static void TestMyReflection()
        {
            System.Type type;

            Person p = new Person();
            type = p.GetType();
            Console.WriteLine("Person type: {0}", type.ToString());

            foreach (MemberInfo member in type.GetMembers())
            {
                Console.WriteLine(member.ToString());
            }

            MethodInfo setMethod = type.GetMethod("set_Name");
            setMethod.Invoke(p, new object[] { "Fred" });

            Console.WriteLine(p.Name);

        }

        public static void TestAssembly()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            List<Type> AccountTypes = new List<Type>();

            foreach (Type t in thisAssembly.GetTypes())
            {
                if (t.IsInterface)
                    continue;

                if (typeof(IAccount).IsAssignableFrom(t))
                {
                    AccountTypes.Add(t);
                }
            }

            var accountTypes = from type in thisAssembly.GetTypes() 
                               where typeof(IAccount).IsAssignableFrom(type) && !type.IsInterface 
                               select type;

            foreach (Type mytype in accountTypes)
            {
                Console.WriteLine(mytype.Name);
            }

        }

        public static void TestCodeDOM()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            // Create a namespace to hold the types we are going to create
            CodeNamespace personnelNameSpace = new CodeNamespace("Personnel");

            // Import the system namespace 
            personnelNameSpace.Imports.Add(new CodeNamespaceImport("System"));
            // Create a Person class
            CodeTypeDeclaration personClass = new CodeTypeDeclaration("Person");
            personClass.IsClass = true;
            personClass.TypeAttributes = System.Reflection.TypeAttributes.Public;

            // Add the Person class to personnelNamespace 
            personnelNameSpace.Types.Add(personClass);

            // Create a field to hold the name of a person
            CodeMemberField nameField = new CodeMemberField("String", "name");
            nameField.Attributes = MemberAttributes.Private;

            // Add the name field to the Person class
            personClass.Members.Add(nameField);

            // Add the namespace to the document 
            compileUnit.Namespaces.Add(personnelNameSpace);

            // Now we need to send our document somewhere
            // Create a provider to parse the document
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            // Give the provider somewhere to send the parsed output
            StringWriter s = new StringWriter();
            // Set some options for the parse - we can use the defaults
            CodeGeneratorOptions options = new CodeGeneratorOptions();

            // Generate the C# source from the CodeDOM
            provider.GenerateCodeFromCompileUnit(compileUnit, s, options);
             // Close the output stream
            s.Close();

            // Print the C# output
            Console.WriteLine(s.ToString());
        }

        public static void TestLamdaExpressionTree()
        {
            // build the expression tree: 
            // Expression<Func<int,int>> square = num => num * num;

            // The parameter for the expression is an integer
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");

            // The opertion to be performed is to square the parameter
            BinaryExpression squareOperation = Expression.Multiply(numParam, numParam);

            // This creates an expression tree that describes the square operation
            Expression<Func<int, int>> square =
                Expression.Lambda<Func<int, int>>(
                    squareOperation,
                    new ParameterExpression[] { numParam });

            // Compile the tree to make an executable method and assign it to a delegate
            Func<int, int> doSquare = square.Compile();

            // Call the delegate
            Console.WriteLine("Square of 2: {0}", doSquare(2));

        }

        public static void TestExpression()
        {
            MultiplyToAdd m = new MultiplyToAdd();

            //Expression<Func<int, int>> addExpression = (Expression<Func<int, int>>)m.Modify(square); 
            //Func<int, int> doAdd = addExpression.Compile();

            //Console.WriteLine("Double of 4: {0}", doAdd(4));

        }

        public static void TestMyAssembly()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Full name: {0}", assembly.FullName);
            AssemblyName name = assembly.GetName();
            Console.WriteLine("Major Version: {0}", name.Version.Major);
            Console.WriteLine("Minor Version: {0}", name.Version.Minor);
            Console.WriteLine("In global assembly cache: {0}",
            assembly.GlobalAssemblyCache);
            foreach (Module assemblyModule in assembly.Modules)
            {
                Console.WriteLine("  Module: {0}", assemblyModule.Name);
                foreach (Type moduleType in assemblyModule.GetTypes())
                {
                    Console.WriteLine("     Type: {0}", moduleType.Name);
                    foreach (MemberInfo member in moduleType.GetMembers())
                    {
                        Console.WriteLine("        Member: {0}", member.Name);
                    }
                }
            }

        }
    }


    public class MultiplyToAdd : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Multiply)
            {
                Expression left = this.Visit(b.Left);
                Expression right = this.Visit(b.Right);

                // Make this binary expression an Add rather than a multiply operation.              
                return Expression.Add(left, right);
            }

            return base.VisitBinary(b);
        }
    }
}
