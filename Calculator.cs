using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MEF_Calculator
{
    [Export(typeof(ICalculator))]
    internal class Calculator : ICalculator
    {
        [ImportMany]
        public IEnumerable<Lazy<IOperation, IOperationData>>? operations;

        public string Calculate(string input)
        {
            char operand = GetOperation(input);
            if (operand == default(char))
            {
                return "Unable to parse command.";
            }

            (int left, int right) operands;
            try
            {
                var indexOfOperand = input.IndexOf(operand);
                operands = GetOperands(input, indexOfOperand);
            }
            catch (ArgumentOutOfRangeException)
            {
                return "Unable to parse command.";
            }
            catch (FormatException)
            {
                return "Unable to parse command.";
            }
            catch (OverflowException)
            {
                return "Unable to parse command.";
            }

            return this.Calculate(operand, operands.left, operands.right);
        }

        private static char GetOperation(string input)
        {
            return input.Where(c => !char.IsDigit(c)).FirstOrDefault();
        }

        private static (int left, int right) GetOperands(string input, int indexOfOperator)
        {
            var leftString = input.Substring(0, indexOfOperator);
            var rightString = input.Substring(indexOfOperator + 1);
            var left = int.Parse(leftString);
            var right = int.Parse(rightString);
            return (left, right);
        }

        private string Calculate(char operand, int left, int right)
        {
            var operation = this.operations?.Where(o => o.Metadata.Symbol.Equals(operand))
                .FirstOrDefault();
            if (operation == null)
            {
                return "Operation not found!";
            }
            else
            {
                return operation.Value.Operate(left, right).ToString("D");
            }
        }
    }
}
