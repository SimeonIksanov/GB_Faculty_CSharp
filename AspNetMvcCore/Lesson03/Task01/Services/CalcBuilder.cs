using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Services
{
    internal class CalcBuilder
    {
        private Calc _calc = new Calc();

        public CalcBuilder AddOperandA(int operand)
        {
            _calc.Operand1 = operand;
            return this;
        }
        public CalcBuilder AddOperandB(int operand)
        {
            _calc.Operand2 = operand;
            return this;
        }
        public CalcBuilder AddOperator(OperationEnum o)
        {
            _calc.Operator = o;
            return this;
        }

        public Calc Buid()
        {
            var t = _calc;
            Reset();
            return t;
        }

        private void Reset()
        {
            _calc = new Calc();
        }
    }
}
