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
    public class ProjektsController : Controller
    {
        private readonly BugProjektContext _context;
        public ProjektsController(BugProjektContext context)
        {
            _context = context;
        }
       
        // GET: Projekts
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Projekts.ToListAsync());
        }

        // GET: Projekts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }
           
            return View(projekt);
        }

        // GET: Projekts/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Projekts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjektId,ProjektName,StartDatum,EndDatum")] Projekt projekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(projekt);
        }

        // GET: Projekts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }
           
            return View(projekt);
        }

        // POST: Projekts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjektId,ProjektName,StartDatum,EndDatum")] Projekt projekt)
        {
            if (id != projekt.ProjektId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Projekt existing = await _context.Projekts.FindAsync(projekt.ProjektId);
                    if (existing == null)
                    {
                        _context.Projekts.Add(projekt);
                    }
                    else
                    {
                        _context.Entry(existing).State = EntityState.Detached;
                        _context.Update(projekt);
                    }
                    _context.Update(projekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjektExists(projekt.ProjektId))
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
           
            return View(projekt);
        }

        // GET: Projekts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _context.Projekts
                .FirstOrDefaultAsync(m => m.ProjektId == id);
            if (projekt == null)
            {
                return NotFound();
            }
           
            return View(projekt);
        }

        // POST: Projekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projekt = await _context.Projekts.FindAsync(id);
            _context.Projekts.Remove(projekt);
            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        private bool ProjektExists(int id)
        {
           
            return _context.Projekts.Any(e => e.ProjektId == id);
        }
    }
}
