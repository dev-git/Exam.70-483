using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    class MyEncapsulation
    {
        public static void MyMain()
        {
            Customer c = new Customer();
            c.Name = "Rob";
            Console.WriteLine("Customer name: {0}", c.Name);


        }
    }

    class Customer
    {
        public string Name;
    }

    class BetterCustomer
    {
        private string _nameValue;

        public string Name
        {
            // Enforce encapsulation by using properties
            get
            {
                return _nameValue;
            }
            set
            {
                if (value == "")
                    throw new Exception("Invalid customer name");
                _nameValue = value;
            }
        }

        /* You can provide “read only” properties by creating properties that only contain a get behavior. 
         * These are useful if you want to expose multiple views of the data in an object */
        public string NickName
        {
            get { return _nameValue + "za"; }
        }
    }

    /* Enforce encapsulation by using accessors */
    class FirstBankAccount
    {
        /* Making a data member of a class private will stop direct access to that data member.
            This will prevent code in any external class from having access to that data member */
        private decimal _accountBalance = 0;
        /*  If you don’t specify an access modifier for a member of a type, 
         *      the access to that member will default to private. */ 

        public void PayInFunds(decimal amountToPayIn)
        {
            _accountBalance = _accountBalance + amountToPayIn;
        }

        public bool WithdrawFunds(decimal amountToWithdraw)
        {
            if (amountToWithdraw > _accountBalance)
                return false;

            _accountBalance = _accountBalance - amountToWithdraw;
            return true;
        }


        public decimal GetBalance()
        {
            return _accountBalance;
        }

        public static void TestBankAccount()
        {
            FirstBankAccount a = new FirstBankAccount();
            a.PayInFunds(50);
            Console.WriteLine("Pay in 50");
            a.PayInFunds(50);
            if (a.WithdrawFunds(10))
                Console.WriteLine("Withdrawn 10");
            Console.WriteLine("Account balance is: {0}", a.GetBalance());

        }
    }

    class BankAccount
    {
        /* The protected access modifier makes a class member useable in any classes 
         * that extend the parent (base) class in which the member is declared */
        protected decimal _accountBalance = 0;

        public void PayInFunds(decimal amountToPayIn)
        {
            _accountBalance = _accountBalance + amountToPayIn;
        }

        public virtual bool WithdrawFunds(decimal amountToWithdraw)
        {
            if (amountToWithdraw > _accountBalance)
                return false;

            _accountBalance = _accountBalance - amountToWithdraw;
            return true;
        }

        public decimal GetBalance()
        {
            return _accountBalance;
        }
    }

    class OverdraftAccount : BankAccount
    {
        decimal overdraftLimit = 100;

        public override bool WithdrawFunds(decimal amountToWithdraw)
        {
            if (amountToWithdraw > _accountBalance + overdraftLimit)
                return false;

            _accountBalance = _accountBalance - amountToWithdraw;
            return true;
        }
    }

    public class RunTest
    {
        public static void TestBankAccount()
        {
            OverdraftAccount a = new OverdraftAccount();
            a.PayInFunds(50);
            Console.WriteLine("Pay in 50");
            if (a.WithdrawFunds(60))
                Console.WriteLine("Withdrawn 60");
            Console.WriteLine("Account balance is: {0}", a.GetBalance());
        }


        /* The internal access modifier will make a member of a type accessible within the assembly
            in which it is declared. This can be an exe or dll*/

        /* The readonly access modifier will make a member of a type read only. The value of the 
        member can only be set at declaration or within the constructor of the class.  */

        public static void TestInterface()
        {
            Report myReport = new Report();
            IPrintable printItem = myReport;
            printItem.GetPrintableText(1, 1);
        }
    }

    interface IPrintable
    {
        string GetPrintableText(int pageWidth, int pageHeight);
        string GetTitle();
    }

    interface IDisplay
    {
        string GetTitle();
    }

    class Report : IPrintable, IDisplay
    {
        // Enforce encapsulation by using explicit interface  implementation
        string IPrintable.GetPrintableText(int pageWidth, int pageHeight)
        {
            return "Report text to be printed";
        }

        string IPrintable.GetTitle()
        {
            return "Report title to be printed";
        }

        string IDisplay.GetTitle()
        {
            return "Report title to be displayed";
        }
    }

}
