using System.Collections.Generic;

namespace Crosswords.Services.Tokenization
{
    public static class TokenizationService
    {
        public static IEnumerable<string> Tokenize(this string text)
        {
            ITokenizer tokenizer = new CzechTokenizer();
            return tokenizer.Tokenize(text);
        }
    }
}