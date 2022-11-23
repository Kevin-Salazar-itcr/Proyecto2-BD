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
    public class VentaCotizacionesTvpsController : Controller
    {
        private readonly CRMContext _context;

        public VentaCotizacionesTvpsController(CRMContext context)
        {
            _context = context;
        }

        // GET: VentaCotizacionesTvps
        public async Task<IActionResult> Index()
        {
              return View(await _context.VentaCotizacionesTvps.ToListAsync());
        }

        // GET: VentaCotizacionesTvps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VentaCotizacionesTvps == null)
            {
                return NotFound();
            }

            var ventaCotizacionesTvp = await _context.VentaCotizacionesTvps
                .FirstOrDefaultAsync(m => m.Cotizacion == id);
            if (ventaCotizacionesTvp == null)
            {
                return NotFound();
            }

            return View(ventaCotizacionesTvp);
        }

        // GET: VentaCotizacionesTvps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VentaCotizacionesTvps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cotizacion,Oportunidad,CuentaAsociada,TotalCotizacion,TotalValorPresente")] VentaCotizacionesTvp ventaCotizacionesTvp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventaCotizacionesTvp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ventaCotizacionesTvp);
        }

        // GET: VentaCotizacionesTvps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VentaCotizacionesTvps == null)
            {
                return NotFound();
            }

            var ventaCotizacionesTvp = await _context.VentaCotizacionesTvps.FindAsync(id);
            if (ventaCotizacionesTvp == null)
            {
                return NotFound();
            }
            return View(ventaCotizacionesTvp);
        }

        // POST: VentaCotizacionesTvps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cotizacion,Oportunidad,CuentaAsociada,TotalCotizacion,TotalValorPresente")] VentaCotizacionesTvp ventaCotizacionesTvp)
        {
            if (id != ventaCotizacionesTvp.Cotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventaCotizacionesTvp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaCotizacionesTvpExists(ventaCotizacionesTvp.Cotizacion))
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
            return View(ventaCotizacionesTvp);
        }

        // GET: VentaCotizacionesTvps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VentaCotizacionesTvps == null)
            {
                return NotFound();
            }

            var ventaCotizacionesTvp = await _context.VentaCotizacionesTvps
                .FirstOrDefaultAsync(m => m.Cotizacion == id);
            if (ventaCotizacionesTvp == null)
            {
                return NotFound();
            }

            return View(ventaCotizacionesTvp);
        }

        // POST: VentaCotizacionesTvps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VentaCotizacionesTvps == null)
            {
                return Problem("Entity set 'CRMContext.VentaCotizacionesTvps'  is null.");
            }
            var ventaCotizacionesTvp = await _context.VentaCotizacionesTvps.FindAsync(id);
            if (ventaCotizacionesTvp != null)
            {
                _context.VentaCotizacionesTvps.Remove(ventaCotizacionesTvp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaCotizacionesTvpExists(string id)
        {
          return _context.VentaCotizacionesTvps.Any(e => e.Cotizacion == id);
        }
    }
}
