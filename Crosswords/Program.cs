using System;
using System.Collections.Generic;
using Crosswords.Services;
using Crosswords.Services.Catalogs;

namespace Crosswords
{
    class Program
    {
        static void Main(string[] args)
        {
            var krSlovnik = new KrSlovnikImporter("/Volumes/[C] Windows 10.hidden/Program Files (x86)/KrSlovnik/");
            krSlovnik.Import();
            
            DictionaryService.Save();
        }
    }
}