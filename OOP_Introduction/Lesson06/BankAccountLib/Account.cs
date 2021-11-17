using System;
namespace BankAccountLib
{
    public class Account
    {
        static private uint _accountNumberCounter = 1;

        private uint _accountNumber;

        private decimal _balance;

        private AccountType _accountType;

        public Account() : this(AccountType.Current)
        {
        }

        public Account(decimal balance) : this(AccountType.Current, balance)
        { }

        public Account(AccountType accountType, Decimal balance = 0)
        {
            _accountType = accountType;
            Balance = balance;
            AccountNumber = _accountNumberCounter;
            ChangeAccountNumber();
        }

        public static bool operator ==(Account a, Account b)
        {
            return a.Equals( b);
        }
        public static bool operator !=(Account a, Account b)
        {
            return !a.Equals(b);
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            private set
            {
                _balance = value;
            }
        }

        public uint AccountNumber
        {
            get { return _accountNumber; }
            private set { _accountNumber = value; }
        }

        public AccountType Type
        {
            get { return _accountType; }
        }

        public void Deposit(decimal value)
        {
            if (value > 0)
            {
                _balance += value;
            }
        }

        public void Withdraw(decimal value)
        {
            if (value > 0 && HasEnoughMoney(value))
            {
                _balance -= value;
            }
        }

        public override string ToString()
        {
            return $"Account number {_accountNumber} of type \"{_accountType}\" has balance {Balance}";
        }

        public bool TakePayment(Account fromAccount, decimal value)
        {
            if (value <= 0) throw new ArgumentException("Payment sum cannot be less then zero", nameof(value));

            if (fromAccount.HasEnoughMoney(value))
            {
                fromAccount.Withdraw(value);
                Balance += value;
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is Account other)
            {
                return Equals(other);
            }

            return false;
        }
        public bool Equals(Account other)
        {
            return this._accountNumber == other._accountNumber;
        }

        private void ChangeAccountNumber()
        {
            _accountNumberCounter++;
        }

        private bool HasEnoughMoney(decimal value)
        {
            return Balance >= value;
        }

        public override int GetHashCode()
        {
            return _accountNumber.GetHashCode();
        }
    }
}
