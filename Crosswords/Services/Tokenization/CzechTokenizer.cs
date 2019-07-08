using System.Collections.Generic;

namespace Crosswords.Services.Tokenization
{
    public class CzechTokenizer : ITokenizer
    {
        public IEnumerable<string> Tokenize(string text)
        {
            while (text.Length > 0)
            {
                string nextToken = text.StartsWith("CH") ? "CH" : text[0].ToString();
                text = text.Substring(nextToken.Length);
                yield return nextToken;
            }
        }
    }
}