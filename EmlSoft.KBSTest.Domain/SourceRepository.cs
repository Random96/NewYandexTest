using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public abstract class SourceRepository : IDisposable, IRWRepository<Source>
    {
        #region Create/Update/Delete
        public abstract void Create(Source t);

        public abstract Task CreateAsync(Source t);

        public abstract void Delete(Source t);

        public abstract Task DeleteAsync(Source t);

        public abstract void Update(Source t);

        public abstract Task UpdateAsync(Source t);
        #endregion

        #region Navigate
        public abstract ICollection<Source> GetList(int From, int PageSize);

        public abstract ICollection<Source> GetListBack(int From, int PageSize);

        public abstract Task<ICollection<Source>> GetListAsync(int From, int PageSize);

        public abstract Task<ICollection<Source>> GetListBackAsync(int From, int PageSize);
        #endregion

        #region IDisposable
        public abstract void Dispose();
        #endregion

    }
}
