﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmlSoft.KBSTest.WebApp.Controllers
{
    public class SearchController : Controller
    {
        readonly Domain.ISourceRepository m_Rep;

        public SearchController(Domain.ISourceRepository Rep )
        {
			m_Rep = Rep ?? throw new ArgumentNullException( nameof(Rep));
        }

        // GET: Search
        public async Task<ActionResult> Index(string SearchStr)
        {
            var ret = await m_Rep.SerachAsync(SearchStr);

            return View(ret);
        }
    }
}