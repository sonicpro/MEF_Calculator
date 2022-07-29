using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MEF_Calculator.Operations;

namespace MEF_Calculator.Calculator
{
    [Export(typeof(ICalculator))]
    internal class Calculator : ICalculator
    {
        [ImportMany]
#pragma warning disable 0649
        public IEnumerable<ExportFactory<IOperation, IOperationData>>? operations;
#pragma warning restore 0649

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
            var operationFactory = this.operations?.FirstOrDefault(o => o.Metadata.Symbol.Equals(operand));
            if (operationFactory == null)
            {
                return "Operation not found!";
            }
            else
            {
                return operationFactory.CreateExport().Value.Operate(left, right).ToString("D");
            }
        }
    }
}
