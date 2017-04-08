using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public interface IRWRepository<T, Key>  : IRepository<T> where T : class
    {
        #region Sync
        void Create(T t);

        void Delete(T t);

        void Update(T t);

        T GetItemById(Key Id);
        #endregion

        #region Async
        Task CreateAsync(Source t);

        Task DeleteAsync(Source t);

        Task UpdateAsync(Source t);

        Task<T> GetItemByIdAsync(Key Id);
        #endregion
    }
}
