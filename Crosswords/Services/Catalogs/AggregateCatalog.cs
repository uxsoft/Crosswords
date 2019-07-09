using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Crosswords.Services.Catalogs
{
    public class AggregateCatalog : ICatalog
    {
        public ICatalog[] Catalogs { get; }

        public AggregateCatalog(params ICatalog[] catalogs)
        {
            Catalogs = catalogs;
        }

        public string? GetLabel(string word)
        {
            return Catalogs
                .Select(c => c.GetLabel(word))
                .FirstOrDefault(s => s != null);
        }

        public IEnumerable<string> SuggestWords()
        {
            throw new NotImplementedException();
        }

        public int GetWordLength(string startsWith)
        {
            return Catalogs
                .Max()
        }
    }
}