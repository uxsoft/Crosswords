using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosswords.Services.Catalogs
{
    public class CsvCatalog : ICatalog
    {
        public CsvCatalog(string file)
        {
            Source = LoadFromFile(file);
        }

        public Dictionary<string, string> Source { get; set; }

        public Dictionary<string, string> LoadFromFile(string file) =>
            File.ReadAllLines(file)
                .Select(l => l.Split(';'))
                .ToDictionary(a => a[0], a => a[1]);


        public string GetLabel(string word) =>
            Source.ContainsKey(word) ? Source[word] : null;

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