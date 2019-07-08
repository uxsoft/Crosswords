using System;
using System.Threading;
using Crosswords.Models;
using Crosswords.Services.Builders;
using Crosswords.Services.Catalogs;

namespace Crosswords.Services
{
    public class CrosswordService
    {
        public static CrosswordPuzzle Build()
        {
            var cts = new CancellationTokenSource();

            var catalog = new AggregateCatalog(new PeriodicTableCatalog(), new RomanNumeralsCatalog());
            
            var puzzle = MonteCarloBuilder.Build(11, 16, catalog, (int)DateTime.Now.Ticks, cts.Token);

            return puzzle;
        }
    }
}