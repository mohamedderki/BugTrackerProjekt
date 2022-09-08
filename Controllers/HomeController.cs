using BugProjektV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugProjektV1.Controllers
{
    public class HomeController : Controller
    {

        private BugProjektContext ctx;
        public HomeController(BugProjektContext ctx)
        {
            this.ctx = ctx;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        
        //public IActionResult BugDetials(int BugId)
        //{
        //    return View(ctx.Bugs.FirstOrDefault(b => b.BugId == BugId));
        //}
        //public IActionResult AddBug()
        //{
        //    return View();
        //}

    }
}
