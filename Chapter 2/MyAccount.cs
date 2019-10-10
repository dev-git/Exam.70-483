using System;
using System.Collections;
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
    public interface IAccount : IComparable<IAccount>
    {
        /* Note that for the use of typed IComparable  to be made to work with objects managed
        by the IAccount interface I had to change the definition of the IAccount interface so that it
        extends the IComparable  interface. */
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
    }

    public class MyBankAccount : IAccount, IComparable<IAccount>
    {
        //private decimal _balance = 0;
        protected decimal _balance = 0;

        public MyBankAccount(int balance = 50)
        {
            _balance = balance;
        }

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

        //public int CompareTo(object obj)  // before typed IComparable
        public int CompareTo(IAccount obj)  // after typed IComparable
        {
            // if we are being compared with a null object we are definitely after it
            if (obj == null) return 1;

            // Convert the object reference into an account reference
            IAccount account = obj as IAccount;

            // as generates null if the conversion fails
            if (account == null)
                throw new ArgumentException("Object is not an account");

            // use the balance value as the basis of the comparison
            return _balance.CompareTo(account.GetBalance());

        }
    }
    /* By giving us a sealed keyword which means “You can’t override this method any more” 
         You can only seal an overriding method and sealing a method does not prevent a child class
        from replacing a method in a parent. However, you can also mark a class as sealed. This means
        that the class cannot be extended, so it cannot be used as the basis for another class. */
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
        public static void TestAbstractAccount()
        {
            SuperAccount superAccount = new SuperAccount();
        }

        public static void TestAccountSort()
        {
            // Create 20 accounts with random balances
            List<IAccount> accounts = new List<IAccount>();
            Random rand = new Random(1);
            for (int i = 0; i < 20; i++)
            {
                IAccount account = new MyBankAccount(rand.Next(0, 10000));
                accounts.Add(account);
            }

            // Sort the accounts
            accounts.Sort();

            // Display the sorted accounts
            foreach (IAccount account in accounts)
            {
                Console.WriteLine(account.GetBalance());
            }
        }

        public static void TestEnumerator()
        {
            // Get an enumerator that can iterate through a string
            var stringEnumerator = "Hello world".GetEnumerator();

            // This prints out the “Hello world” string one character at a time
            while (stringEnumerator.MoveNext())
            {
                Console.Write(stringEnumerator.Current);
            }
        }

        public static void TestEnumThing()
        {
            EnumeratorThing e = new EnumeratorThing(10);

            foreach (int i in e)
                Console.WriteLine(i);
        }

        public static void TestBetterEnumThing()
        {
            BetterEnumeratorThing e = new BetterEnumeratorThing(10);

            foreach (int i in e)
                Console.WriteLine(i);
        }

        public static void TestMyYield()
        {
            foreach (int i in Integers())
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static IEnumerable<int> Integers()
        {
            yield return 1;
            yield return 2;
            yield return 4;
            yield return 8;
            yield return 16;
            yield return 16777216;
        }
    }

    class SuperAccount : AbsBankAccount
    {
        // It is not possible to make an instance of an abstract   class
        public override string WarningLetterString()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AbsBankAccount
    {
        /* abstract classes are different from interfaces in that they can contain 
         * fully implemented methods alongside the abstract ones */
        public abstract string WarningLetterString();
    }

    /* This implements both the IEnumerable interface (meaning it can be enumerated) and
        the IEnumerator<int> interface (meaning that it contains a call of GetEnumerator
        to get an enumerator from it) */
    class EnumeratorThing : IEnumerator<int>, IEnumerable
    {
        int count;
        int limit;

        public int Current
        {
            get
            {
                return count;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return count;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++count == limit)
                return false;
            else
                return true;
        }

        public void Reset()
        {
            count = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public EnumeratorThing(int limit)
        {
            count = 0;
            this.limit = limit;
        }
    }

    class BetterEnumeratorThing : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 1; i < 10; i++)
                yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int limit;
        public BetterEnumeratorThing(int limit)
        {
            this.limit = limit;
        }


    }
}
