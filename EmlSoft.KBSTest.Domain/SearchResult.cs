using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public class SearchResult : ISearchResult
    {
        public string Url { get; set; }

        public string Result { get; set; }
    }
}
