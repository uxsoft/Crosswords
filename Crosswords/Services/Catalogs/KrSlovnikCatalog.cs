using System;
using System.IO;
using System.Linq;

namespace Crosswords.Services.Catalogs
{
    public class KrSlovnikImporter
    {
        public string Folder { get; }

        public KrSlovnikImporter(string folder)
        {
            Folder = folder;
        }

        public void Import()
        {
            Directory.GetFiles(Folder, "*.dat")
                .Select(ImportDatFile)
                .Sum();
        }

        public int ImportDatFile(string path)
        {
            var contents = File.ReadAllText(path);
            var entries = contents.Split(';', StringSplitOptions.RemoveEmptyEntries);
            return entries
                .Select(e => e.Split('@'))
                .Where(e => e.Length == 2)
                .Select(e => DictionaryService.Dictionary.TryAdd(e[0], e[1]))
                .Count();
        }
    }
}