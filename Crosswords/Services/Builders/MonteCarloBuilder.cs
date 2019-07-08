using System;
using System.Linq;
using System.Threading;
using Crosswords.Models;
using Crosswords.Services.Catalogs;
using Crosswords.Services.Tokenization;

namespace Crosswords.Services.Builders
{
    public class MonteCarloBuilder
    {
        public static CrosswordPuzzle Build(int width, int height, ICatalog catalog, int? seed = null,
            CancellationToken? cancel = null)
        {
            CrosswordPuzzle puzzle = new CrosswordPuzzle(width, height);

            // start filling words LtR
            for (int i = 0; i <= width * height; i++)
            {
                int x = i % width, y = i / width;

                var currentWordDown = puzzle.ReadDownInReverse(x, y);
                if (catalog.GetWordLength(currentWordDown) == currentWordDown.Length)
                {
                    // the word down can't be continued, add a header, continue
                    puzzle.Grid[x, y] = new Header();
                    continue;
                }

                var word = FindFittingWord(puzzle, catalog, x, y);

                if (word == null)
                    return null; // no word fits gotta try again

                puzzle.AddWordAcross(word, x, y);
                i += word.Length;
            }

            puzzle.FillHeaders(catalog.GetLabel);
            return puzzle;
        }

        private static string? FindFittingWord(CrosswordPuzzle puzzle, ICatalog catalog, int x, int y)
        {
            foreach (var word in catalog.SuggestWords())
            {
                var fits = word.Tokenize().Select((token, i) =>
                    {
                        var wordFragmentDown = puzzle.ReadDownInReverse(x + i, y);
                        var wordProposedDown = wordFragmentDown + token;

                        var length = catalog.GetWordLength(wordProposedDown);

                        return length >= wordProposedDown.Length;
                    })
                    .All(b => b);

                if (fits)
                    return word;
            }

            return null;
        }
    }
}