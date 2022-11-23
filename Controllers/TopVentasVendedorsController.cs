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
    public class TopVentasVendedorsController : Controller
    {
        private readonly CRMContext _context;

        public TopVentasVendedorsController(CRMContext context)
        {
            _context = context;
        }

        // GET: TopVentasVendedors
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopVentasVendedors.ToListAsync());
        }

        // GET: TopVentasVendedors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TopVentasVendedors == null)
            {
                return NotFound();
            }

            var topVentasVendedor = await _context.TopVentasVendedors
                .FirstOrDefaultAsync(m => m.Vendedor == id);
            if (topVentasVendedor == null)
            {
                return NotFound();
            }

            return View(topVentasVendedor);
        }

        // GET: TopVentasVendedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopVentasVendedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vendedor,VentaTotal")] TopVentasVendedor topVentasVendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topVentasVendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topVentasVendedor);
        }

        // GET: TopVentasVendedors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TopVentasVendedors == null)
            {
                return NotFound();
            }

            var topVentasVendedor = await _context.TopVentasVendedors.FindAsync(id);
            if (topVentasVendedor == null)
            {
                return NotFound();
            }
            return View(topVentasVendedor);
        }

        // POST: TopVentasVendedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Vendedor,VentaTotal")] TopVentasVendedor topVentasVendedor)
        {
            if (id != topVentasVendedor.Vendedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topVentasVendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopVentasVendedorExists(topVentasVendedor.Vendedor))
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
            return View(topVentasVendedor);
        }

        // GET: TopVentasVendedors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TopVentasVendedors == null)
            {
                return NotFound();
            }

            var topVentasVendedor = await _context.TopVentasVendedors
                .FirstOrDefaultAsync(m => m.Vendedor == id);
            if (topVentasVendedor == null)
            {
                return NotFound();
            }

            return View(topVentasVendedor);
        }

        // POST: TopVentasVendedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TopVentasVendedors == null)
            {
                return Problem("Entity set 'CRMContext.TopVentasVendedors'  is null.");
            }
            var topVentasVendedor = await _context.TopVentasVendedors.FindAsync(id);
            if (topVentasVendedor != null)
            {
                _context.TopVentasVendedors.Remove(topVentasVendedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopVentasVendedorExists(string id)
        {
          return _context.TopVentasVendedors.Any(e => e.Vendedor == id);
        }
    }
}
