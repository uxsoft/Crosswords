using System.Collections;
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
        
        public string GetLabel(string word)
        {
            Catalogs.FirstOrDefault()
        }
    }
}