using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public interface ISearchResult
    {
        string Url { get; set; }

        string Result { get; set; }
    }
}
