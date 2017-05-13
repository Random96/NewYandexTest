using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmlSoft.KBSTest.WebApp.ModelView
{
    public class SourceModelView : ISourceModelView
    {
        readonly Domain.ISourceRepository m_Rep;

        const int PageSize = 5;
        public SourceModelView(Domain.ISourceRepository Rep)
        {
            if (Rep == null)
                throw new ArgumentNullException("Rep");

            m_Rep = Rep;
        }


        public async Task<IEnumerable<Domain.Source>> GetIndexAsync(int Id, int Direction)
        {
            IEnumerable<Domain.Source> ret = null;
            switch (Direction)
            {
                case 1:
                    ret = await m_Rep.GetListAsync(Id, PageSize);
                    break;

                case -1:
                    ret = await m_Rep.GetListBackAsync(Id, PageSize);
                    break;

                default:
                    ret = new List<Domain.Source>();
                    break;
            }
            return ret;
        }

        public async Task CreateAsync(string sName)
        {
            await m_Rep.CreateAsync(new Domain.Source { Url = sName });
        }


        public async Task<Domain.Source> GetItemByIdAsync(int Id)
        {
            return await m_Rep.GetItemByIdAsync(Id);
        }

        public async Task UpdateAsync(Domain.Source Item)
        {
            await m_Rep.UpdateAsync(Item);
        }

        public async Task DeleteAsync(int Id)
        {
            var Item = await m_Rep.GetItemByIdAsync(Id);

            if ( Item != null)
                await m_Rep.DeleteAsync( Item );
        }
    }
}