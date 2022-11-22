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
    public class VistaVentasCotXdeptoesController : Controller
    {
        private readonly CRMContext _context;

        public VistaVentasCotXdeptoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaVentasCotXdeptoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaVentasCotXdeptos.ToListAsync());
        }

        // GET: VistaVentasCotXdeptoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaVentasCotXdeptos == null)
            {
                return NotFound();
            }

            var vistaVentasCotXdepto = await _context.VistaVentasCotXdeptos
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (vistaVentasCotXdepto == null)
            {
                return NotFound();
            }

            return View(vistaVentasCotXdepto);
        }

        // GET: VistaVentasCotXdeptoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaVentasCotXdeptoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,NumeroCotizacion,Venta")] VistaVentasCotXdepto vistaVentasCotXdepto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaVentasCotXdepto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaVentasCotXdepto);
        }

        // GET: VistaVentasCotXdeptoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaVentasCotXdeptos == null)
            {
                return NotFound();
            }

            var vistaVentasCotXdepto = await _context.VistaVentasCotXdeptos.FindAsync(id);
            if (vistaVentasCotXdepto == null)
            {
                return NotFound();
            }
            return View(vistaVentasCotXdepto);
        }

        // POST: VistaVentasCotXdeptoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,NumeroCotizacion,Venta")] VistaVentasCotXdepto vistaVentasCotXdepto)
        {
            if (id != vistaVentasCotXdepto.NumeroCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaVentasCotXdepto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaVentasCotXdeptoExists(vistaVentasCotXdepto.NumeroCotizacion))
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
            return View(vistaVentasCotXdepto);
        }

        // GET: VistaVentasCotXdeptoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaVentasCotXdeptos == null)
            {
                return NotFound();
            }

            var vistaVentasCotXdepto = await _context.VistaVentasCotXdeptos
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (vistaVentasCotXdepto == null)
            {
                return NotFound();
            }

            return View(vistaVentasCotXdepto);
        }

        // POST: VistaVentasCotXdeptoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaVentasCotXdeptos == null)
            {
                return Problem("Entity set 'CRMContext.VistaVentasCotXdeptos'  is null.");
            }
            var vistaVentasCotXdepto = await _context.VistaVentasCotXdeptos.FindAsync(id);
            if (vistaVentasCotXdepto != null)
            {
                _context.VistaVentasCotXdeptos.Remove(vistaVentasCotXdepto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaVentasCotXdeptoExists(string id)
        {
          return _context.VistaVentasCotXdeptos.Any(e => e.NumeroCotizacion == id);
        }
    }
}
