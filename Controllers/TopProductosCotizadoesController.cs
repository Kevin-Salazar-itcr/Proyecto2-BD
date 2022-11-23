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
    public class TopProductosCotizadoesController : Controller
    {
        private readonly CRMContext _context;

        public TopProductosCotizadoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TopProductosCotizadoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopProductosCotizados.ToListAsync());
        }

        // GET: TopProductosCotizadoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TopProductosCotizados == null)
            {
                return NotFound();
            }

            var topProductosCotizado = await _context.TopProductosCotizados
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (topProductosCotizado == null)
            {
                return NotFound();
            }

            return View(topProductosCotizado);
        }

        // GET: TopProductosCotizadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopProductosCotizadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Precio,VecesCotizado")] TopProductosCotizado topProductosCotizado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topProductosCotizado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topProductosCotizado);
        }

        // GET: TopProductosCotizadoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TopProductosCotizados == null)
            {
                return NotFound();
            }

            var topProductosCotizado = await _context.TopProductosCotizados.FindAsync(id);
            if (topProductosCotizado == null)
            {
                return NotFound();
            }
            return View(topProductosCotizado);
        }

        // POST: TopProductosCotizadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Descripcion,Precio,VecesCotizado")] TopProductosCotizado topProductosCotizado)
        {
            if (id != topProductosCotizado.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topProductosCotizado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopProductosCotizadoExists(topProductosCotizado.Nombre))
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
            return View(topProductosCotizado);
        }

        // GET: TopProductosCotizadoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TopProductosCotizados == null)
            {
                return NotFound();
            }

            var topProductosCotizado = await _context.TopProductosCotizados
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (topProductosCotizado == null)
            {
                return NotFound();
            }

            return View(topProductosCotizado);
        }

        // POST: TopProductosCotizadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TopProductosCotizados == null)
            {
                return Problem("Entity set 'CRMContext.TopProductosCotizados'  is null.");
            }
            var topProductosCotizado = await _context.TopProductosCotizados.FindAsync(id);
            if (topProductosCotizado != null)
            {
                _context.TopProductosCotizados.Remove(topProductosCotizado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopProductosCotizadoExists(string id)
        {
          return _context.TopProductosCotizados.Any(e => e.Nombre == id);
        }
    }
}
