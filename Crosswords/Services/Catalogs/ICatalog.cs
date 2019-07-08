using System.Collections;
using System.Collections.Generic;

namespace Crosswords.Services.Catalogs
{
    public interface ICatalog
    {
        string? GetLabel(string word);

        IEnumerable<string> SuggestWords();

        int GetWordLength(string startsWith);
    }
}