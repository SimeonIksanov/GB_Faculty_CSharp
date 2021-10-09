using System;
namespace Lesson02
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
            if (value > 0 && _balance >= value)
            {
                _balance -= value;
            }
        }


        public override string ToString()
        {
            return $"Account number {_accountNumber} of type \"{_accountType}\" has balance {Balance}";
        }

        private void ChangeAccountNumber()
        {
            _accountNumberCounter++;
        }

    }
}
