using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    interface IRWRepository<T>  : IRepository<T> where T : class
    {
        #region Sync
        void Create(T t);

        void Delete(T t);

        void Update(T t);
        #endregion

        #region Async
        Task CreateAsync(Source t);

        Task DeleteAsync(Source t);

        Task UpdateAsync(Source t);
        #endregion
    }
}
