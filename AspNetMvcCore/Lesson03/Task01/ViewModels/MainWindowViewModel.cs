using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Task01.Commands;
using Task01.Services;

namespace Task01.ViewModels
{
    internal partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _title = "Calculator";
        private int _operand1 = 0;
        private int _operand2 = 0;
        private OperatorComboboxItem _operation = OperatorsComboboxItems[0];
        private string _result = "0";
        private ICommand? _calcCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }
        public static List<OperatorComboboxItem> OperatorsComboboxItems { get; set; } = new() {
            new OperatorComboboxItem {Name='+',Operation = OperationEnum.Sum },
            new OperatorComboboxItem {Name='-',Operation = OperationEnum.Dif },
            new OperatorComboboxItem {Name='*',Operation = OperationEnum.Mul },
            new OperatorComboboxItem {Name='/',Operation = OperationEnum.Div }
        };
        public string Operand1
        {
            get
            {
                return _operand1.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out _operand1 ))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Operand1)));
                }
            }
        }
        public string Operand2
        {
            get
            {
                return _operand2.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out _operand2))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Operand2)));
                }
            }
        }
        public OperatorComboboxItem Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Operation)));
            }
        }
        public string Result
        {
            get
            {
                return _result;
            }
            private set
            {
                _result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        public ICommand CalcCommand
        {
            get 
            {
                return _calcCommand
                    ??= new LambdaCommand(OnCalculateCommandExecuted, CanCalculateCommandExecute);
            }
        }
        internal class OperatorComboboxItem
        {
            public char Name { get; set; }
            public OperationEnum Operation { get; set; }
        }

        private  bool CanCalculateCommandExecute(object? parameter)
        {
            return _operation.Operation != OperationEnum.Div
                || (_operation.Operation == OperationEnum.Div && _operand2 != 0);
        }
        private void OnCalculateCommandExecuted(object? parameter)
        {
            var calc = new CalcBuilder()
                .AddOperandA(_operand1)
                .AddOperandB(_operand2)
                .AddOperator(_operation.Operation)
                .Buid();
            try
            {
                var r = calc.Calculate();
                Result = r.ToString();
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
        }

    }
}
