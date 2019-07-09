using System.Collections.Generic;
using System.Text;

namespace Crosswords.Services.Catalogs
{
    public class RomanNumeralsCatalog : ICatalog
    {
        private readonly string[] _thousands = {"", "M", "MM", "MMM"};
        private readonly string[] _hundreds = {"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"};
        private readonly string[] _tens = {"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"};
        private readonly string[] _ones = {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"};

        /// <summary>
        /// Convert integer to roman numerals
        /// </summary>
        /// <param name="arabic"></param>
        /// <returns></returns>
        private string ArabicToRoman(int arabic)
        {
            // See if it's >= 4000.
            if (arabic >= 4000)
            {
                // Use parentheses.
                int thousands = arabic / 1000;
                arabic %= 1000;
                return $"({ArabicToRoman(thousands)}) {ArabicToRoman(arabic)}";
            }

            // Otherwise process the letters.
            var sb = new StringBuilder();

            // Pull out thousands.
            int num = arabic / 1000;
            sb.Append(_thousands[num]);
            arabic %= 1000;

            // Handle hundreds.
            num = arabic / 100;
            sb.Append(_hundreds[num]);
            arabic %= 100;

            // Handle tens.
            num = arabic / 10;
            sb.Append(_tens[num]);
            arabic %= 10;

            // Handle ones.
            sb.Append(_ones[arabic]);

            return sb.ToString();
        }

        // Maps letters to numbers.
        private readonly Dictionary<char, int> _charValues = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        /// <summary>
        /// Convert Roman numerals to an integer
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        private int RomanToArabic(string roman)
        {
            if (roman.Length == 0) return 0;
            roman = roman.ToUpper();

            // See if the number begins with (.
            if (roman[0] == '(')
            {
                // Find the closing parenthesis.
                int pos = roman.LastIndexOf(')');

                // Get the value inside the parentheses.
                string part1 = roman.Substring(1, pos - 1);
                string part2 = roman.Substring(pos + 1);
                return 1000 * RomanToArabic(part1) + RomanToArabic(part2);
            }

            // The number doesn't begin with (.
            // Convert the letters' values.
            int total = 0;
            int lastValue = 0;
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                int newValue = _charValues[roman[i]];

                // See if we should add or subtract.
                if (newValue < lastValue)
                    total -= newValue;
                else
                {
                    total += newValue;
                    lastValue = newValue;
                }
            }

            // Return the result.
            return total;
        }

        public string GetLabel(string word)
        {
            return $"Římsky '{RomanToArabic(word)}'";
        }

        public IEnumerable<string> SuggestWords()
        {
            throw new System.NotImplementedException();
        }

        public int GetWordLength(string startsWith)
        {
            throw new System.NotImplementedException();
        }
    }
}