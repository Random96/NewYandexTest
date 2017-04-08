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
        readonly Domain.SourceRepository m_Rep;

        public SourceController(Domain.SourceRepository Rep)
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
                    ret = await m_Rep.GetListAsync(Id, 15);
                    break;

                case -1:
                    ret = await m_Rep.GetListBackAsync(Id, 15);
                    break;

                default:
                    return View(new List<Domain.Source>());
            }
            return View(ret);
        }

        // GET: Source/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Source/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                await m_Rep.UpdateAsync( new Domain.Source { Id = id, Url = "qq" });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Source/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Source/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
