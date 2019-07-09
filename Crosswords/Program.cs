using System;
using System.Collections.Generic;
using Crosswords.Services;
using Crosswords.Services.Builders;
using Crosswords.Services.Catalogs;

namespace Crosswords
{
    class Program
    {
        static void Main(string[] args)
        {
            CrosswordService.Build();
        }
    }
}