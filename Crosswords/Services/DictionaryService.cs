using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Crosswords.Services
{
    public static class DictionaryService
    {
        static DictionaryService()
        {
            Load();
        }
        
        public static Dictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>();

        public static void Load()
        {
            using StreamReader sr = new StreamReader(GetDictionaryFilePath());

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line != null)
                {
                    var items = line.Split(';');
                    Dictionary.TryAdd(items[0], items[1]);
                }
            }
        }

        internal static string GetDictionaryFilePath() =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "dict.csv");

        public static void Save()
        {
            Save(Dictionary);
        }

        public static void Save(Dictionary<string, string> dict)
        {
            using StreamWriter sw = new StreamWriter(GetDictionaryFilePath(), false);

            foreach (var pair in dict)
            {
                sw.WriteLine($"{pair.Key};{pair.Value}");
            }
        }
    }
}