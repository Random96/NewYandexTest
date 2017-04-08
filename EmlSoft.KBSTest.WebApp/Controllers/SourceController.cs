using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmlSoft.KBSTest.WebApp.Controllers
{
    public class SourceController : Controller
    {
        readonly Domain.ISourceRepository m_Rep;

        const int PageSize = 5;

        public SourceController(Domain.ISourceRepository Rep)
        {
            if (Rep == null)
                throw new ArgumentNullException("Rep");

            m_Rep = Rep;
        }

        // GET: Source
        public async Task<ActionResult> Index(int Id = 0, int Direction = 1)
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
                    return View(new List<Domain.Source>());
            }
            return View(ret);
        }

        // GET: Source/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Source/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await m_Rep.CreateAsync(new Domain.Source { Url = collection["Url"] });

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(new Domain.Source { Url = collection["Url"] } );
        }

        // GET: Source/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var Src = await m_Rep.GetItemByIdAsync(id);

            return View( Src );
        }

        // POST: Source/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int ? id , FormCollection collection)
        {
            if( id == null )
                return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    await m_Rep.UpdateAsync(new Domain.Source { Id = id ?? 0, Url = collection["Url"] });

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(new Domain.Source { Url = collection["Url"] });
        }

        // GET: Source/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Src = await m_Rep.GetItemByIdAsync(id);

            return View(Src);
        }

        // POST: Source/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int ? id, FormCollection collection)
        {
            if( id == null  )
                return RedirectToAction("Index");

            try
            {
                if (ModelState.IsValid)
                {
                    await m_Rep.DeleteAsync(new Domain.Source { Id = id ?? 0, Url = collection["Url"] });

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message );
            }

            return View(new Domain.Source { Url = collection["Url"] });
        }
    }
}
