using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task01.ViewModels.MainWindowViewModel;

namespace Task01.Services
{
    internal class Calc
    {
        public int Operand1 { get; set; } = 1;
        public int Operand2 { get; set; } = 1;
        public OperationEnum Operator { get; set; } = OperationEnum.Sum;

        public double Calculate()
        {
            checked
            {
                switch (Operator)
                {
                    case OperationEnum.Sum:
                        return Operand1 + Operand2;
                    case OperationEnum.Dif:
                        return Operand1 - Operand2;
                    case OperationEnum.Mul:
                        return Operand1 * Operand2;
                    case OperationEnum.Div:
                        return Operand1 / (double)Operand2;
                    default:
                        return double.NaN;
                }
            }
        }

    }
}
