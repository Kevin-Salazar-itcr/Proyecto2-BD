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
    public class VistaVentaZonasController : Controller
    {
        private readonly CRMContext _context;

        public VistaVentaZonasController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaVentaZonas
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaVentaZonas.ToListAsync());
        }

        // GET: VistaVentaZonas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaVentaZonas == null)
            {
                return NotFound();
            }

            var vistaVentaZona = await _context.VistaVentaZonas
                .FirstOrDefaultAsync(m => m.Zona == id);
            if (vistaVentaZona == null)
            {
                return NotFound();
            }

            return View(vistaVentaZona);
        }

        // GET: VistaVentaZonas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaVentaZonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Zona,Venta")] VistaVentaZona vistaVentaZona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaVentaZona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaVentaZona);
        }

        // GET: VistaVentaZonas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaVentaZonas == null)
            {
                return NotFound();
            }

            var vistaVentaZona = await _context.VistaVentaZonas.FindAsync(id);
            if (vistaVentaZona == null)
            {
                return NotFound();
            }
            return View(vistaVentaZona);
        }

        // POST: VistaVentaZonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Zona,Venta")] VistaVentaZona vistaVentaZona)
        {
            if (id != vistaVentaZona.Zona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaVentaZona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaVentaZonaExists(vistaVentaZona.Zona))
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
            return View(vistaVentaZona);
        }

        // GET: VistaVentaZonas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaVentaZonas == null)
            {
                return NotFound();
            }

            var vistaVentaZona = await _context.VistaVentaZonas
                .FirstOrDefaultAsync(m => m.Zona == id);
            if (vistaVentaZona == null)
            {
                return NotFound();
            }

            return View(vistaVentaZona);
        }

        // POST: VistaVentaZonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaVentaZonas == null)
            {
                return Problem("Entity set 'CRMContext.VistaVentaZonas'  is null.");
            }
            var vistaVentaZona = await _context.VistaVentaZonas.FindAsync(id);
            if (vistaVentaZona != null)
            {
                _context.VistaVentaZonas.Remove(vistaVentaZona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaVentaZonaExists(string id)
        {
          return _context.VistaVentaZonas.Any(e => e.Zona == id);
        }
    }
}
