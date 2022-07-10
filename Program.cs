﻿using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEF_Calculator
{
    class Program
    {
        private CompositionContainer _container;

#pragma warning disable 8618
        private Program()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            this._container = new CompositionContainer(catalog);
            this._container.ComposeParts(this);
        }
#pragma warning restore 8618

        static void Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine("Enter Command:");
            while (true)
            {
                var s = Console.ReadLine();
                Console.WriteLine(p.calculator.Calculate(s));
            }

        }

        [Import(typeof(ICalculator))]
#pragma warning disable 0649
        public ICalculator calculator;
#pragma warning restore 0649
    }
}