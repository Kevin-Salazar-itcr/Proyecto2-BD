using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;

namespace ProyectoCRM.Controllers
{
    public class VistasCotTipoesController : Controller
    {
        private readonly CRMContext _context;

        public VistasCotTipoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistasCotTipoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistasCotTipos.ToListAsync());
        }

        // GET: VistasCotTipoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistasCotTipos == null)
            {
                return NotFound();
            }

            var vistasCotTipo = await _context.VistasCotTipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (vistasCotTipo == null)
            {
                return NotFound();
            }

            return View(vistasCotTipo);
        }

        // GET: VistasCotTipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistasCotTipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Total")] VistasCotTipo vistasCotTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistasCotTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistasCotTipo);
        }

        // GET: VistasCotTipoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistasCotTipos == null)
            {
                return NotFound();
            }

            var vistasCotTipo = await _context.VistasCotTipos.FindAsync(id);
            if (vistasCotTipo == null)
            {
                return NotFound();
            }
            return View(vistasCotTipo);
        }

        // POST: VistasCotTipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Tipo,Total")] VistasCotTipo vistasCotTipo)
        {
            if (id != vistasCotTipo.Tipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistasCotTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistasCotTipoExists(vistasCotTipo.Tipo))
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
            return View(vistasCotTipo);
        }

        // GET: VistasCotTipoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistasCotTipos == null)
            {
                return NotFound();
            }

            var vistasCotTipo = await _context.VistasCotTipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (vistasCotTipo == null)
            {
                return NotFound();
            }

            return View(vistasCotTipo);
        }

        // POST: VistasCotTipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistasCotTipos == null)
            {
                return Problem("Entity set 'CRMContext.VistasCotTipos'  is null.");
            }
            var vistasCotTipo = await _context.VistasCotTipos.FindAsync(id);
            if (vistasCotTipo != null)
            {
                _context.VistasCotTipos.Remove(vistasCotTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistasCotTipoExists(string id)
        {
          return _context.VistasCotTipos.Any(e => e.Tipo == id);
        }
    }
}
