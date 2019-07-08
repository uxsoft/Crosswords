using System.Collections.Generic;

namespace Crosswords.Services.Tokenization
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string s);
    }
}