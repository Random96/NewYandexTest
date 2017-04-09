using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EmlSoft.KBSTest.Domain;

namespace EmlSoft.KBSTest.Data
{
    public class SqlSourceRepository : Domain.ISourceRepository
    {
        readonly SqlContext m_Context;

        public SqlSourceRepository(SqlContext Context)
        {
            m_Context = Context;
        }

        public void Create(Domain.Source t)
        {
            Source src = new Source(){Url = t.Url};
            m_Context.Sources.Add(src);
            m_Context.SaveChanges();
        }

        public async Task CreateAsync(Domain.Source t)
        {
            Source src = new Source() { Url = t.Url };
            m_Context.Sources.Add(src);
            await m_Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            m_Context.Dispose();
        }

        public ICollection<Domain.Source> GetList(int From, int PageSize)
        {
            if(PageSize == 0)
                return m_Context.Sources.AsNoTracking().OrderBy(p => p.Id).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToList();

            return m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p=>p.Id).Take(PageSize).Select(p => new Domain.Source { Id=p.Id, Url=p.Url }).ToList();
        }

        public ICollection<Domain.Source> GetListBack(int From, int PageSize)
        {
            var ret = m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToList();
            ret.Reverse();
            return ret;
        }

        public async Task<ICollection<Domain.Source>> GetListAsync(int From, int PageSize)
        {
            ICollection<Domain.Source> ret = null;
            if(PageSize == 0)
                ret = await m_Context.Sources.AsNoTracking().OrderBy(p => p.Id).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            else
                ret = await  m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            return ret;
        }

        public async Task<ICollection<Domain.Source>> GetListBackAsync(int From, int PageSize)
        {
            var ret = await m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            ret.Reverse();
            return ret;
        }

        public async Task DeleteAsync(Domain.Source Src)
        {
            if (Src == null)
                throw new ArgumentNullException("Src");

            var ret = await m_Context.Sources.FirstOrDefaultAsync(p => p.Id == Src.Id);

            if (ret == null)
                throw new Exception($"Not found id {Src.Id}");

            m_Context.Sources.Remove(ret);

            await m_Context.SaveChangesAsync();
        }

        public void Delete(Domain.Source Src)
        {
            if (Src == null)
                throw new ArgumentNullException("Src");

            var ret = m_Context.Sources.FirstOrDefault(p => p.Id == Src.Id);

            if (ret == null)
                throw new Exception($"Not found id {Src.Id}");

            m_Context.Sources.Remove(ret);

            m_Context.SaveChanges();
        }

        public void Update(Domain.Source Src)
        {
            if (Src == null)
                throw new ArgumentNullException("Src");

            var ret = m_Context.Sources.FirstOrDefault(p => p.Id == Src.Id);

            if (ret == null)
                throw new Exception($"Not found id {Src.Id}");

            ret.Url = Src.Url;

            m_Context.SaveChanges();
        }


        public async Task UpdateAsync(Domain.Source Src)
        {
            if (Src == null)
                throw new ArgumentNullException("Src");

            var ret = await m_Context.Sources.FirstOrDefaultAsync(p => p.Id == Src.Id);

            if (ret == null)
                throw new Exception($"Not found id {Src.Id}");

            ret.Url = Src.Url;

            await m_Context.SaveChangesAsync();
        }

        public Domain.Source GetItemById(int Id)
        {
            var ret = m_Context.Sources.AsNoTracking().FirstOrDefault(p => p.Id == Id);

            if (ret == null)
                return new Domain.Source();

            return new Domain.Source()
            {
                Id = ret.Id,
                Url = ret.Url
            };
        }

        public async Task<Domain.Source> GetItemByIdAsync(int Id)
        {
            var ret = await m_Context.Sources.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);

            if (ret == null)
                return new Domain.Source();

            return new Domain.Source()
            {
                Id = ret.Id,
                Url = ret.Url
            };
        }

        public void SaveSourceContext(int SourceId, string Context)
        {
            var ret = m_Context.Contents.FirstOrDefault(p => p.SourceId == SourceId);
            if(ret == null)
            {
                ret = new Content()
                {
                    SourceId = SourceId
                };
                m_Context.Contents.Add(ret);
            }
            ret.Data = Context;
            m_Context.SaveChanges();
        }
    }
}
