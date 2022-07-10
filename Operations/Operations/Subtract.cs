using System.ComponentModel.Composition;

namespace MEF_Calculator.Operations.Operations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    internal class Subtract : IOperation
    {
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }
}
