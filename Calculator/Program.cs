using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using static MEF_Calculator.Calculator.DirectoryHelper;

namespace MEF_Calculator.Calculator
{
    class Program
    {
        private readonly CompositionContainer _container;

        private Program()
        {
            using var catalog = new AggregateCatalog();
            var extensionsPath = GetAbsolutePath("Extensions");
            foreach (var dir in Directory.EnumerateDirectories(extensionsPath))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(dir));
                // ICalculator dependency is loaded from Assembly catalog.
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            }
            this._container = new CompositionContainer(catalog);
            this._container.ComposeParts(this);
        }

        static void Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine("Enter Command:");
            while (true)
            {
                var s = Console.ReadLine();
                Console.WriteLine(p.calculator?.Calculate(s!));
            }
        }

        [Import(typeof(ICalculator))]
#pragma warning disable 0649
        private ICalculator? calculator;
#pragma warning restore 0649
    }
}
