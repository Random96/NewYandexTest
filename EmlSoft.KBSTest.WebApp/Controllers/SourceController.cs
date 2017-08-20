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
        ModelView.ISourceModelView m_ModelView;

        public SourceController(ModelView.ISourceModelView ModelView )
        {
            m_ModelView = ModelView ?? throw new ArgumentNullException( nameof(ModelView));
		}

        // GET: Source
        public async Task<ActionResult> Index(int Id = 0, int Direction = 1)
        {
            IEnumerable<Domain.Source> ret = await m_ModelView.GetIndexAsync(Id, Direction);

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
                    await m_ModelView.CreateAsync(collection["Url"]);

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(new Domain.Source { Url = collection["Url"] } );
        }

        // GET: Source/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var Src = await m_ModelView.GetItemByIdAsync(id);

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
                    await m_ModelView.UpdateAsync(new Domain.Source { Id = id ?? 0, Url = collection["Url"] });

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
            var Src = await m_ModelView.GetItemByIdAsync(id);

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
                    await m_ModelView.DeleteAsync(id ?? 0);

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
