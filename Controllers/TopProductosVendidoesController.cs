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
    public class TopProductosVendidoesController : Controller
    {
        private readonly CRMContext _context;

        public TopProductosVendidoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TopProductosVendidoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopProductosVendidos.ToListAsync());
        }

        // GET: TopProductosVendidoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TopProductosVendidos == null)
            {
                return NotFound();
            }

            var topProductosVendido = await _context.TopProductosVendidos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (topProductosVendido == null)
            {
                return NotFound();
            }

            return View(topProductosVendido);
        }

        // GET: TopProductosVendidoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopProductosVendidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,VecesVendido")] TopProductosVendido topProductosVendido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topProductosVendido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topProductosVendido);
        }

        // GET: TopProductosVendidoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TopProductosVendidos == null)
            {
                return NotFound();
            }

            var topProductosVendido = await _context.TopProductosVendidos.FindAsync(id);
            if (topProductosVendido == null)
            {
                return NotFound();
            }
            return View(topProductosVendido);
        }

        // POST: TopProductosVendidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Descripcion,VecesVendido")] TopProductosVendido topProductosVendido)
        {
            if (id != topProductosVendido.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topProductosVendido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopProductosVendidoExists(topProductosVendido.Nombre))
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
            return View(topProductosVendido);
        }

        // GET: TopProductosVendidoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TopProductosVendidos == null)
            {
                return NotFound();
            }

            var topProductosVendido = await _context.TopProductosVendidos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (topProductosVendido == null)
            {
                return NotFound();
            }

            return View(topProductosVendido);
        }

        // POST: TopProductosVendidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TopProductosVendidos == null)
            {
                return Problem("Entity set 'CRMContext.TopProductosVendidos'  is null.");
            }
            var topProductosVendido = await _context.TopProductosVendidos.FindAsync(id);
            if (topProductosVendido != null)
            {
                _context.TopProductosVendidos.Remove(topProductosVendido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopProductosVendidoExists(string id)
        {
          return _context.TopProductosVendidos.Any(e => e.Nombre == id);
        }
    }
}
