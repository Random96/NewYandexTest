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

        public GraberController(Domain.ISourceRepository Rep, Domain.IGrubber Gruber)
        {
			m_Rep = Rep ?? throw new ArgumentNullException(nameof(Rep));

            m_Gruber = Gruber ?? throw new ArgumentNullException(nameof(Gruber));
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
                var Src = m_Rep.GetItemById(Id);
                try
                {
                    string Context = m_Gruber.Grub(Src.Url);

                    m_Rep.SaveSourceContext(Id, Context);

                    return Json(new { Status = 1, Name = Src.Url, Comment = string.Empty });
                }
                catch (Exception ex)
                {
                    return Json(new { Status = 0, Name = Src.Url, Comment = ex.Message });
                }
            }
            return RedirectToAction("Index");
        }

    }
}