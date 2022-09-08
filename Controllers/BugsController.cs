using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugProjektV1.Models;

namespace BugProjektV1.Controllers
{
    public class BugsController : Controller
    {
        private readonly BugProjektContext _context;

        public BugsController(BugProjektContext context)
        {
            _context = context;
        }
        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            var bugProjektContext = _context.Bugs.Include(b => b.Entwickler).Include(b => b.Projekt).Include(b => b.Tester);
           
            return View(await bugProjektContext.ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.Entwickler)
                .Include(b => b.Projekt)
                .Include(b => b.Tester)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }
          
            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            ViewData["EntwicklerId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Bereich == "Entwickler"), "MitarbeiterId", "Vorname");
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "ProjektName");
            ViewData["TesterId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Bereich == "Tester"), "MitarbeiterId", "Vorname");
           
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,Titel,Beschreibung,ErfassungDatum,BehebungsDatum,TesterId,EntwicklerId,ProjektId")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntwicklerId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.EntwicklerId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "ProjektName", bug.ProjektId);
            ViewData["TesterId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.TesterId);
          
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            ViewData["EntwicklerId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.EntwicklerId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "ProjektName", bug.ProjektId);
            ViewData["TesterId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.TesterId);
           
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,Titel,Beschreibung,ErfassungDatum,BehebungsDatum,TesterId,EntwicklerId,ProjektId")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Bug existing = await _context.Bugs.FindAsync(bug.BugId);
                    if (existing == null)
                    {
                        _context.Bugs.Add(bug);
                    }
                    else
                    {
                        _context.Entry(existing).State = EntityState.Detached;
                        _context.Update(bug);
                    }
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.BugId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntwicklerId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.EntwicklerId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "ProjektName", bug.ProjektId);
            ViewData["TesterId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Vorname", bug.TesterId);
           
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.Entwickler)
                .Include(b => b.Projekt)
                .Include(b => b.Tester)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }
            

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
       
            return RedirectToAction(nameof(Index));

        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
