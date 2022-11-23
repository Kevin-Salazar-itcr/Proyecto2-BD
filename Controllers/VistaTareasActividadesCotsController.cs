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
    public class VistaTareasActividadesCotsController : Controller
    {
        private readonly CRMContext _context;

        public VistaTareasActividadesCotsController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaTareasActividadesCots
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaTareasActividadesCots.ToListAsync());
        }

        // GET: VistaTareasActividadesCots/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaTareasActividadesCots == null)
            {
                return NotFound();
            }

            var vistaTareasActividadesCot = await _context.VistaTareasActividadesCots
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (vistaTareasActividadesCot == null)
            {
                return NotFound();
            }

            return View(vistaTareasActividadesCot);
        }

        // GET: VistaTareasActividadesCots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaTareasActividadesCots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCotizacion,Total")] VistaTareasActividadesCot vistaTareasActividadesCot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaTareasActividadesCot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaTareasActividadesCot);
        }

        // GET: VistaTareasActividadesCots/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaTareasActividadesCots == null)
            {
                return NotFound();
            }

            var vistaTareasActividadesCot = await _context.VistaTareasActividadesCots.FindAsync(id);
            if (vistaTareasActividadesCot == null)
            {
                return NotFound();
            }
            return View(vistaTareasActividadesCot);
        }

        // POST: VistaTareasActividadesCots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroCotizacion,Total")] VistaTareasActividadesCot vistaTareasActividadesCot)
        {
            if (id != vistaTareasActividadesCot.NumeroCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaTareasActividadesCot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaTareasActividadesCotExists(vistaTareasActividadesCot.NumeroCotizacion))
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
            return View(vistaTareasActividadesCot);
        }

        // GET: VistaTareasActividadesCots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaTareasActividadesCots == null)
            {
                return NotFound();
            }

            var vistaTareasActividadesCot = await _context.VistaTareasActividadesCots
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (vistaTareasActividadesCot == null)
            {
                return NotFound();
            }

            return View(vistaTareasActividadesCot);
        }

        // POST: VistaTareasActividadesCots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaTareasActividadesCots == null)
            {
                return Problem("Entity set 'CRMContext.VistaTareasActividadesCots'  is null.");
            }
            var vistaTareasActividadesCot = await _context.VistaTareasActividadesCots.FindAsync(id);
            if (vistaTareasActividadesCot != null)
            {
                _context.VistaTareasActividadesCots.Remove(vistaTareasActividadesCot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaTareasActividadesCotExists(string id)
        {
          return _context.VistaTareasActividadesCots.Any(e => e.NumeroCotizacion == id);
        }
    }
}
