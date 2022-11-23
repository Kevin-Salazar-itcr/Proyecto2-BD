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
    public class VistaVentaSectorsController : Controller
    {
        private readonly CRMContext _context;

        public VistaVentaSectorsController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaVentaSectors
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaVentaSectors.ToListAsync());
        }

        // GET: VistaVentaSectors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaVentaSectors == null)
            {
                return NotFound();
            }

            var vistaVentaSector = await _context.VistaVentaSectors
                .FirstOrDefaultAsync(m => m.Sector == id);
            if (vistaVentaSector == null)
            {
                return NotFound();
            }

            return View(vistaVentaSector);
        }

        // GET: VistaVentaSectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaVentaSectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sector,Venta")] VistaVentaSector vistaVentaSector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaVentaSector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaVentaSector);
        }

        // GET: VistaVentaSectors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaVentaSectors == null)
            {
                return NotFound();
            }

            var vistaVentaSector = await _context.VistaVentaSectors.FindAsync(id);
            if (vistaVentaSector == null)
            {
                return NotFound();
            }
            return View(vistaVentaSector);
        }

        // POST: VistaVentaSectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Sector,Venta")] VistaVentaSector vistaVentaSector)
        {
            if (id != vistaVentaSector.Sector)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaVentaSector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaVentaSectorExists(vistaVentaSector.Sector))
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
            return View(vistaVentaSector);
        }

        // GET: VistaVentaSectors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaVentaSectors == null)
            {
                return NotFound();
            }

            var vistaVentaSector = await _context.VistaVentaSectors
                .FirstOrDefaultAsync(m => m.Sector == id);
            if (vistaVentaSector == null)
            {
                return NotFound();
            }

            return View(vistaVentaSector);
        }

        // POST: VistaVentaSectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaVentaSectors == null)
            {
                return Problem("Entity set 'CRMContext.VistaVentaSectors'  is null.");
            }
            var vistaVentaSector = await _context.VistaVentaSectors.FindAsync(id);
            if (vistaVentaSector != null)
            {
                _context.VistaVentaSectors.Remove(vistaVentaSector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaVentaSectorExists(string id)
        {
          return _context.VistaVentaSectors.Any(e => e.Sector == id);
        }
    }
}
