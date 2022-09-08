using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugProjektV1.Models;
using Microsoft.AspNetCore.Authorization;

namespace BugProjektV1.Controllers
{
    [Authorize]
    public class MitarbeitersController : Controller
    {
        private readonly BugProjektContext _context;
       
        public MitarbeitersController(BugProjektContext context)
        {
            _context = context;
        }

        // GET: Mitarbeiters
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Mitarbeiters.ToListAsync());

        }

        // GET: Mitarbeiters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiters
                .FirstOrDefaultAsync(m => m.MitarbeiterId == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }
            
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Create
        public IActionResult Create()
        {
            
            return View();
           
        }

        // POST: Mitarbeiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MitarbeiterId,Vorname,Nachname,Bereich")] Mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mitarbeiter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiters.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }
            
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MitarbeiterId,Vorname,Nachname,Bereich")] Mitarbeiter mitarbeiter)
        {
            if (id != mitarbeiter.MitarbeiterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Mitarbeiter existing = await _context.Mitarbeiters.FindAsync(mitarbeiter.MitarbeiterId);
                    if (existing == null)
                    {
                        _context.Mitarbeiters.Add(mitarbeiter);
                    }
                    else
                    {
                        _context.Entry(existing).State = EntityState.Detached;
                        _context.Update(mitarbeiter);
                    }
                    _context.Update(mitarbeiter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MitarbeiterExists(mitarbeiter.MitarbeiterId))
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
            
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Bugs.Where(b=> b.TesterId == id | b.EntwicklerId == id).Any())
            {
                return View("NotDeletebale");
            }
            var mitarbeiter = await _context.Mitarbeiters
                .FirstOrDefaultAsync(m => m.MitarbeiterId == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mitarbeiter = await _context.Mitarbeiters.FindAsync(id);
            _context.Mitarbeiters.Remove(mitarbeiter);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool MitarbeiterExists(int id)
        {
            return _context.Mitarbeiters.Any(e => e.MitarbeiterId == id);
        }
    }
}
