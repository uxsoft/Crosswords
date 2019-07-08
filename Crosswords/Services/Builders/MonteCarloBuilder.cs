using System;
using System.Threading;
using Crosswords.Models;
using Crosswords.Services.Catalogs;

namespace Crosswords.Services.Builders
{
    public class MonteCarloBuilder
    {
        public static CrosswordPuzzle Build(int width, int height, ICatalog catalog, int? seed = null,
            CancellationToken? cancel = null)
        {
            CrosswordPuzzle puzzle = new CrosswordPuzzle(width, height);

            for (int i = 0; i <= width * height; i++)
            {
                int x = i % width, y = i / width;

                if (puzzle.Grid[x, y] is CrosswordPuzzleHeader)
                    continue; // the word TtB can't be continued, checks added a header, continue
                
                var word = FindFittingWord(puzzle, catalog, x, y);

                if (word == null)
                    return null; // no word fits gotta try again
                
                puzzle.AddWordLtR(word, x, y);
                i += word.Length;
            }

            puzzle.FillHeaders(catalog.GetLabel);
            return puzzle;
        }

        private static string FindFittingWord(CrosswordPuzzle puzzle, ICatalog catalog, int x, int y)
        {
            foreach (var word in catalog.SuggestWords())
            {
                        
            }
        }
    }
}