using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EmlSoft.KBSTest.Domain;

namespace EmlSoft.KBSTest.Data
{
    public class SqlSourceRepository : Domain.SourceRepository
    {
        readonly SqlContext m_Context;

        public SqlSourceRepository(SqlContext Context)
        {
            m_Context = Context;
        }

        public override void Create(Domain.Source t)
        {
            Source src = new Source(){Url = t.Url};
            m_Context.Sources.Add(src);
            m_Context.SaveChanges();
        }

        public override async Task CreateAsync(Domain.Source t)
        {
            Source src = new Source() { Url = t.Url };
            m_Context.Sources.Add(src);
            await m_Context.SaveChangesAsync();
        }

        public override void Delete(Domain.Source t)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            m_Context.Dispose();
        }

        public override ICollection<Domain.Source> GetList(int From, int PageSize)
        {
            return m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p=>p.Id).Take(PageSize).Select(p => new Domain.Source { Id=p.Id, Url=p.Url }).ToList();
        }

        public override ICollection<Domain.Source> GetListBack(int From, int PageSize)
        {
            var ret = m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToList();
            ret.Reverse();
            return ret;
        }

        public override async Task<ICollection<Domain.Source>> GetListAsync(int From, int PageSize)
        {
            var ret = await  m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            return ret;
        }

        public override async Task<ICollection<Domain.Source>> GetListBackAsync(int From, int PageSize)
        {
            var ret = await m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            ret.Reverse();
            return ret;
        }

        public override void Update(Domain.Source t)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(Domain.Source t)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Domain.Source t)
        {
            throw new NotImplementedException();
        }
    }
}
