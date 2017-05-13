using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.WebApp.ModelView
{
    public interface ISourceModelView
    {
        Task<IEnumerable<Domain.Source>> GetIndexAsync(int Id, int Direction);

        Task CreateAsync(string sName);

        Task<Domain.Source> GetItemByIdAsync(int Id);

        Task UpdateAsync(Domain.Source Item);

        Task DeleteAsync(int Id);
    }

}
