using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public interface ISourceRepository : IDisposable, IRWRepository<Source, int>
    {
        void SaveSourceContext(int SourceId, string Context);

        Task<IEnumerable<ISearchResult>> SerachAsync(string Search);
    }
}
