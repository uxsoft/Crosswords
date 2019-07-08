using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Linq;
using Crosswords.Services.Tokenization;

namespace Crosswords.Models
{
    public class CrosswordPuzzle
    {
        public int Width { get; }
        public int Height { get; }

        public CrosswordPuzzle(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new ICrosswordPuzzleCell[width, height];

            // whole top is headers
            for (int x = 0; x < width; x++)
                Grid[x, 0] = new CrosswordPuzzleHeader();

            // whole left is headers
            for (int y = 0; y < width; y++)
                Grid[0, y] = new CrosswordPuzzleHeader();
        }

        public ICrosswordPuzzleCell[,] Grid { get; }

        public bool AddWordLtR(string text, int x, int y)
        {
            try
            {
                if (!(Grid[x - 1, y] is CrosswordPuzzleHeader))
                {
                    Grid[x - 1, y] = new CrosswordPuzzleHeader();
                }

                foreach (var token in text.Tokenize())
                {
                    Grid[x, y] = new CrosswordPuzzleLetter {Letter = token};
                    x++;
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }

        public string ReadLtR(int x, int y)
        {
            var letters = Enumerable
                .Range(x, Width - x - 1)
                .Select(x => Grid[x, y])
                .TakeWhile(c => c is CrosswordPuzzleLetter)
                .OfType<CrosswordPuzzleLetter>()
                .Select(c => c.Letter);

            return string.Join("", letters);
        }

        public string ReadTtB(int x, int y)
        {
            var letters = Enumerable
                .Range(y, Height - y - 1)
                .Select(y => Grid[x, y])
                .TakeWhile(c => c is CrosswordPuzzleLetter)
                .OfType<CrosswordPuzzleLetter>()
                .Select(c => c.Letter);

            return string.Join("", letters);
        }

        public void FillHeaders(Func<string, string> getHeader)
        {
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                if (Grid[x, y] is CrosswordPuzzleHeader)
                {
                    var cell = (CrosswordPuzzleHeader) Grid[x, y];

                    var right = ReadLtR(x + 1, y);
                    cell.Right = string.IsNullOrWhiteSpace(right) ? "" : getHeader(right);

                    var down = ReadTtB(x, y + 1);
                    cell.Down = string.IsNullOrWhiteSpace(down) ? "" : getHeader(down);
                }
        }
    }
}