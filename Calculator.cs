using System.ComponentModel.Composition;

namespace MEF_Calculator
{
    [Export(typeof(ICalculator))]
    internal class Calculator : ICalculator
    {
        public string Calculate(string input)
        {
            return "12";
        }
    }
}
