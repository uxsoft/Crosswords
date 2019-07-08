using System.Collections.Generic;
using System.Linq;

namespace Crosswords.Services.Tokenization
{
    public class EnglishTokenizer : ITokenizer
    {
        public IEnumerable<string> Tokenize(string s)
        {
            return s.ToCharArray().Select(c => c.ToString());
        }
    }
}