using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models2;

namespace ProyectoCRM.Controllers
{
    public class CotXtipoesController : Controller
    {
        private readonly CRMContext _context;

        public CotXtipoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: CotXtipoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.CotXtipos.ToListAsync());
        }

        // GET: CotXtipoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CotXtipos == null)
            {
                return NotFound();
            }

            var cotXtipo = await _context.CotXtipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (cotXtipo == null)
            {
                return NotFound();
            }

            return View(cotXtipo);
        }

        // GET: CotXtipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CotXtipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Cantidad")] CotXtipo cotXtipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotXtipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cotXtipo);
        }

        // GET: CotXtipoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CotXtipos == null)
            {
                return NotFound();
            }

            var cotXtipo = await _context.CotXtipos.FindAsync(id);
            if (cotXtipo == null)
            {
                return NotFound();
            }
            return View(cotXtipo);
        }

        // POST: CotXtipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Tipo,Cantidad")] CotXtipo cotXtipo)
        {
            if (id != cotXtipo.Tipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotXtipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotXtipoExists(cotXtipo.Tipo))
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
            return View(cotXtipo);
        }

        // GET: CotXtipoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CotXtipos == null)
            {
                return NotFound();
            }

            var cotXtipo = await _context.CotXtipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (cotXtipo == null)
            {
                return NotFound();
            }

            return View(cotXtipo);
        }

        // POST: CotXtipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CotXtipos == null)
            {
                return Problem("Entity set 'CRMContext.CotXtipos'  is null.");
            }
            var cotXtipo = await _context.CotXtipos.FindAsync(id);
            if (cotXtipo != null)
            {
                _context.CotXtipos.Remove(cotXtipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotXtipoExists(string id)
        {
          return _context.CotXtipos.Any(e => e.Tipo == id);
        }
    }
}
