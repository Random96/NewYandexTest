using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmlSoft.KBSTest.WebApp.Controllers
{
    public class GraberController : Controller
    {
        readonly Domain.ISourceRepository m_Rep;
        readonly Domain.IGrubber m_Gruber;

        public GraberController(Domain.ISourceRepository Rep, Domain.IGrubber m_Gruber)
        {
            if (Rep == null)
                throw new ArgumentNullException("Rep");

            m_Rep = Rep;
        }

        // GET: Graber
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetList()
        {
            if (Request.IsAjaxRequest())
            {
                var ret = (await m_Rep.GetListAsync(0, 0)).Select(p => p.Id).ToArray();
                return Json(ret);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DoGrub(int Id)
        {
            if (Request.IsAjaxRequest())
            {
                return Json( new { Status = 1, Name = "qq" } );
            }
            return RedirectToAction("Index");
        }

    }
}