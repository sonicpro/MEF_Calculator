using System.ComponentModel.Composition;

namespace MEF_Calculator.Operations
{
    [Export(typeof(IOperation))]
    // IOperationData-implementing class is implicitely created based on the "value" parameter of ExportMetadata attribute.
    [ExportMetadata("Symbol", "+")]
    internal class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }
}
