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
            Grid = new ICell[width, height];

            // whole top is headers
            for (int x = 0; x < width; x++)
                Grid[x, 0] = new Header();

            // whole left is headers
            for (int y = 0; y < width; y++)
                Grid[0, y] = new Header();
        }

        public ICell[,] Grid { get; }

        public bool AddWordAcross(string text, int x, int y)
        {
            try
            {
                if (!(Grid[x - 1, y] is Header))
                {
                    Grid[x - 1, y] = new Header();
                }

                foreach (var token in text.Tokenize())
                {
                    Grid[x, y] = new Letter {Token = token};
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

        public string ReadAcross(int x, int y)
        {
            if (Grid[x, y] is Header)
                return ReadAcross(x + 1, y);

            var letters = Enumerable
                .Range(x, Width - x - 1)
                .Select(x => Grid[x, y])
                .TakeWhile(c => c is Letter)
                .OfType<Letter>()
                .Select(c => c.Token);

            return string.Join("", letters);
        }

        public string ReadDown(int x, int y)
        {
            if (Grid[x, y] is Header)
                return ReadAcross(x, y + 1);

            var letters = Enumerable
                .Range(y, Height - y - 1)
                .Select(y => Grid[x, y])
                .TakeWhile(c => c is Letter)
                .OfType<Letter>()
                .Select(c => c.Token);

            return string.Join("", letters);
        }
        
        public string ReadAcrossInReverse(int x, int y)
        {
            var letters = Enumerable
                .Range(0, x)
                .Reverse()
                .Select(x => Grid[x, y])
                .TakeWhile(c => c is Letter)
                .OfType<Letter>()
                .Select(c => c.Token);

            return string.Join("", letters);
        }

        public string ReadDownInReverse(int x, int y)
        {
            var letters = Enumerable
                .Range(0, y)
                .Reverse()
                .Select(y => Grid[x, y])
                .TakeWhile(c => c is Letter)
                .OfType<Letter>()
                .Select(c => c.Token);

            return string.Join("", letters);
        }

        public void FillHeaders(Func<string, string> getHeader)
        {
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                if (Grid[x, y] is Header)
                {
                    var cell = (Header) Grid[x, y];

                    var right = ReadAcross(x + 1, y);
                    cell.Across = string.IsNullOrWhiteSpace(right) ? "" : getHeader(right);

                    var down = ReadDown(x, y + 1);
                    cell.Down = string.IsNullOrWhiteSpace(down) ? "" : getHeader(down);
                }
        }

        public Header? GetHeaderAcross(int x, int y)
        {
            for (int xi = x; xi > 0; xi--)
                if (Grid[xi, y] is Header)
                    return (Header) Grid[xi, y];

            return null;
        }

        public Header GetHeaderDown(int x, int y)
        {
            for (int yi = y; yi > 0; yi--)
                if (Grid[x, yi] is Header)
                    return (Header) Grid[x, yi];

            return null;
        }
        
        
    }
}