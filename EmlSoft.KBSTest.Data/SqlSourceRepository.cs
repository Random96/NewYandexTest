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
        private SqlContext m_Context;

		/// <summary>
		/// default constructor
		/// </summary>
		/// <param name="Context">database context</param>
        public SqlSourceRepository(SqlContext Context)
        {
            m_Context = Context ?? throw new ArgumentNullException( nameof(Context));
        }

		/// <summary>
		/// create new data object in sync mode
		/// </summary>
		/// <param name="t">Source domain object </param>
        public void Create(Domain.Source t)
        {
			Checkisposed();

			Source src = new Source(){Url = t.Url};
            m_Context.Sources.Add(src);
            m_Context.SaveChanges();
        }

		/// <summary>
		/// create new data object in async mode
		/// </summary>
		/// <param name="t">Source domain object </param>
		/// <returns></returns>
		public async Task CreateAsync(Domain.Source t)
        {
			Checkisposed();

			Source src = new Source() { Url = t.Url };
            m_Context.Sources.Add(src);
            await m_Context.SaveChangesAsync();
        }

		/// <summary>
		/// Get list of database object with paging support  in sync mode ordering by id reda forward
		/// </summary>
		/// <param name="From">Id first elements</param>
		/// <param name="PageSize">number item in page. if 0 - read all </param>
		/// <returns></returns>
        public ICollection<Domain.Source> GetList(int From, int PageSize)
        {
			Checkisposed();

			if (PageSize == 0)
                return m_Context.Sources.AsNoTracking().OrderBy(p => p.Id).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToList();

            return m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p=>p.Id).Take(PageSize).Select(p => new Domain.Source { Id=p.Id, Url=p.Url }).ToList();
        }

        public ICollection<Domain.Source> GetListBack(int From, int PageSize)
        {
			Checkisposed();

			var ret = m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToList();
            ret.Reverse();
            return ret;
        }

        public async Task<ICollection<Domain.Source>> GetListAsync(int From, int PageSize)
        {
			Checkisposed();

			ICollection<Domain.Source> ret = null;
            if(PageSize == 0)
                ret = await m_Context.Sources.AsNoTracking().OrderBy(p => p.Id).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            else
                ret = await  m_Context.Sources.AsNoTracking().Where(p => p.Id > From).OrderBy(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            return ret;
        }

        public async Task<ICollection<Domain.Source>> GetListBackAsync(int From, int PageSize)
        {
			Checkisposed();

			var ret = await m_Context.Sources.AsNoTracking().Where(p => p.Id < From).OrderByDescending(p => p.Id).Take(PageSize).Select(p => new Domain.Source { Id = p.Id, Url = p.Url }).ToListAsync();
            ret.Reverse();
            return ret;
        }

        public async Task DeleteAsync(Domain.Source Src)
        {
			Checkisposed();

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
			Checkisposed();

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
			Checkisposed();

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
			Checkisposed();

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
			Checkisposed();

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
			Checkisposed();

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

        public async Task<IEnumerable<ISearchResult>> SerachAsync(string Search)
        {
			Checkisposed();

			const int iPageSize = 200;

            List<SearchResult> ret = new List<SearchResult>();

            if (string.IsNullOrWhiteSpace(Search))
                return ret;

            try
            {
                var List = await m_Context.Contents.AsNoTracking().ToListAsync();

                foreach (var src in List)
                {
                    int Pos = 0;
                    int NewPos;
                    int Max = src.Data.Length;

                    var Source = await m_Context.Sources.FirstOrDefaultAsync(p => p.Id == src.SourceId);

                    while ((NewPos = src.Data.IndexOf(Search, Pos, StringComparison.CurrentCultureIgnoreCase)) > 0)
                    {
                        int FirstPos = NewPos > iPageSize ? NewPos - iPageSize : 0;
                        int Length = FirstPos + iPageSize * 2 + Search.Length > Max ? Max - FirstPos - 1 : FirstPos + iPageSize * 2;
                        string str = src.Data.Substring(FirstPos, Length);
                        ret.Add(new SearchResult { Url = Source.Url, Result = str });
                        Pos = NewPos + 1;
                    }
                }
            }
            catch( Exception ex)
            {
                ret.Add(new SearchResult { Url = null, Result = ex.Message });
            }
            return ret;
        }

		#region IDisposable Support
		private bool disposedValue = false; // Для определения избыточных вызовов

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					m_Context = null;
				}

				disposedValue = true;
			}
		}


		// Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
		public void Dispose()
		{
			// Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
			Dispose(true);
			// TODO: раскомментировать следующую строку, если метод завершения переопределен выше.

			// GC.SuppressFinalize(this);
		}

		private void Checkisposed()
		{
			if (disposedValue)
				throw new ObjectDisposedException( ToString() );
		}
		#endregion
	}
}
