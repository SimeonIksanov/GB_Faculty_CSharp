using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object?> _onExecute;
        private readonly Func<object?, bool> _onCanExecute;

        public LambdaCommand(Action<object?> OnExecute, Func<object?,bool> OnCanExecute)
        {
            _onExecute = OnExecute;
            _onCanExecute = OnCanExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            return _onCanExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object? parameter)
        {
            _onExecute?.Invoke(parameter);
        }
    }
}
