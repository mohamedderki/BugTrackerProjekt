using Microsoft.AspNetCore.Mvc;
using BugProjektV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugProjektV1.Components
{
    public class BudgetsView : ViewComponent
    {

        private BugProjektContext _context;
        public int MA { get; set; }
        public int PA { get; set; }
        public int BA { get; set; }
        public BudgetsView(BugProjektContext ctx)
        {
            _context = ctx;
            MA = _context.Mitarbeiters.ToList().Count();
            PA = _context.Projekts.ToList().Count();
            BA = _context.Bugs.ToList().Count();
        }
        public IViewComponentResult Invoke()
        {
            return View(this);
        }
    }
}