using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetList(int From, int PageSize);

        ICollection<T> GetListBack(int From, int PageSize);

        Task<ICollection<T>> GetListAsync(int From, int PageSize);

        Task<ICollection<T>> GetListBackAsync(int From, int PageSize);

    }
}
