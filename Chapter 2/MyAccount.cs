using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Seventy_483.Csl.Chapter2
{
    /* So, with interfaces you are moving away from considering classes in terms of what they are,
        and starting to think about them in terms of what they can do. In the case of your bank, this
        means that you want to deal with objects in terms of IAccount, (the set of account abilities)
        rather than BankAccount (a particular account class). */
    public interface IAccount
    {
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
    }

    public class MyBankAccount : IAccount
    {
        //private decimal _balance = 0;
        protected decimal _balance = 0;
        /* The WithdrawFunds method in the BabyAccount class makes use of the _balance  value
            that is declared in the parent BankAccount class. To make this possible the _balance 
            value has had its access modifier changed from private to protected so that it can be
            used in classes that extend the BankAccount class. */

        // Original
        //bool IAccount.WithdrawFunds(decimal amount)
        //{
        //    if (_balance < amount)
        //    {
        //        return false;
        //    }
        //    _balance = _balance - amount;
        //    return true;
        //}

        /* The C# compiler needs to know if a method is going to be overridden. This is because
            it must call an overridden method in a slightly different way from a “normal” one. The 
            WithDrawFunds method in the BankAccount class has been declared as virtual so that
            the compiler knows it may be overridden. It might be overridden in classes which are
            children of the parent class.  */

        public virtual bool WithdrawFunds(decimal amount)
        {
            if (_balance < amount)
            {
                return false;
            }
            _balance = _balance - amount;
            return true;
        }

        /* The C# language does not allow the overriding of explicit implementations of interface
            methods. This means that you have to sacrifice a slight measure of encapsulation 
        in order to use class hierarchies in this manner. Here you can see that the
         WithDrawFunds method in the BankAccount class is declared as virtual, but it has not
        been declared as an interface method */

        void IAccount.PayInFunds(decimal amount)
        {
            _balance = _balance + amount;
        }

        decimal IAccount.GetBalance()
        {
            return _balance;
        }
    }
    /* by giving us a sealed keyword which means “You can’t override this method any more” */
    // public sealed class BabyAccount : CustomerAccount,IAccount 
    public class BabyAccount : MyBankAccount, IAccount
    {
        //private decimal _balance = 0;

        //bool IAccount.WithdrawFunds(decimal amount)
        //{
        //    if (amount > 10)
        //    {
        //        return false;
        //    }
        //    if (_balance < amount)
        //    {
        //        return false;
        //    }
        //    _balance = _balance - amount;
        //    return true;
        //}

        /*public override bool WithdrawFundsOverride(decimal amount)
        {
            if (amount > 10)
            {
                return false;
            }

            //if (_balance < amount)
            //{
            //    return false;
            //}
            //_balance = _balance - amount;
            //return true;
            return base.WithdrawFunds(amount);
        }*/

    public new bool WithdrawFunds(decimal amount)
        {
            if (amount > 10)
            {
                return false;
            }
            if (_balance < amount)
            {
                return false;
            }
            _balance = _balance - amount;
            return true;
        }

        void IAccount.PayInFunds(decimal amount)
        {
            _balance = _balance + amount;
        }

        decimal IAccount.GetBalance()
        {
            return _balance;
        }
    }
    class MyAccount
    {
        public static void TestMyAccount()
        {
            IAccount account = new MyBankAccount();
            account.PayInFunds(50);
            Console.WriteLine("Balance: " + account.GetBalance());
        }

        public static void TestBabyAccount()
        {
            IAccount b = new BabyAccount();
            //b.PayInFunds(50);
            b.WithdrawFunds(5);

            if (b is IAccount)
                Console.WriteLine("this object can be used as an account");

            /*  if a cast cannot be performed, a program will throw an exception,
                whereas if the as operator fails it returns a null reference and the program continues running */
             IAccount y = b as IAccount;

        }
    }
}
