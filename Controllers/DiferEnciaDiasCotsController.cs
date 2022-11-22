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
    public class DiferEnciaDiasCotsController : Controller
    {
        private readonly CRMContext _context;

        public DiferEnciaDiasCotsController(CRMContext context)
        {
            _context = context;
        }

        // GET: DiferEnciaDiasCots
        public async Task<IActionResult> Index()
        {
              return View(await _context.DiferEnciaDiasCots.ToListAsync());
        }

        // GET: DiferEnciaDiasCots/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DiferEnciaDiasCots == null)
            {
                return NotFound();
            }

            var diferEnciaDiasCot = await _context.DiferEnciaDiasCots
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (diferEnciaDiasCot == null)
            {
                return NotFound();
            }

            return View(diferEnciaDiasCot);
        }

        // GET: DiferEnciaDiasCots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiferEnciaDiasCots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCotizacion,NombreOportunidad,NombreCuenta,DíasDeDiferencía")] DiferEnciaDiasCot diferEnciaDiasCot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diferEnciaDiasCot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diferEnciaDiasCot);
        }

        // GET: DiferEnciaDiasCots/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DiferEnciaDiasCots == null)
            {
                return NotFound();
            }

            var diferEnciaDiasCot = await _context.DiferEnciaDiasCots.FindAsync(id);
            if (diferEnciaDiasCot == null)
            {
                return NotFound();
            }
            return View(diferEnciaDiasCot);
        }

        // POST: DiferEnciaDiasCots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroCotizacion,NombreOportunidad,NombreCuenta,DíasDeDiferencía")] DiferEnciaDiasCot diferEnciaDiasCot)
        {
            if (id != diferEnciaDiasCot.NumeroCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diferEnciaDiasCot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiferEnciaDiasCotExists(diferEnciaDiasCot.NumeroCotizacion))
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
            return View(diferEnciaDiasCot);
        }

        // GET: DiferEnciaDiasCots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DiferEnciaDiasCots == null)
            {
                return NotFound();
            }

            var diferEnciaDiasCot = await _context.DiferEnciaDiasCots
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (diferEnciaDiasCot == null)
            {
                return NotFound();
            }

            return View(diferEnciaDiasCot);
        }

        // POST: DiferEnciaDiasCots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DiferEnciaDiasCots == null)
            {
                return Problem("Entity set 'CRMContext.DiferEnciaDiasCots'  is null.");
            }
            var diferEnciaDiasCot = await _context.DiferEnciaDiasCots.FindAsync(id);
            if (diferEnciaDiasCot != null)
            {
                _context.DiferEnciaDiasCots.Remove(diferEnciaDiasCot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiferEnciaDiasCotExists(string id)
        {
          return _context.DiferEnciaDiasCots.Any(e => e.NumeroCotizacion == id);
        }
    }
}
