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
    public class VistaVentasDepartamentoesController : Controller
    {
        private readonly CRMContext _context;

        public VistaVentasDepartamentoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaVentasDepartamentoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaVentasDepartamentos.ToListAsync());
        }

        // GET: VistaVentasDepartamentoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaVentasDepartamentos == null)
            {
                return NotFound();
            }

            var vistaVentasDepartamento = await _context.VistaVentasDepartamentos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (vistaVentasDepartamento == null)
            {
                return NotFound();
            }

            return View(vistaVentasDepartamento);
        }

        // GET: VistaVentasDepartamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaVentasDepartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Venta")] VistaVentasDepartamento vistaVentasDepartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaVentasDepartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaVentasDepartamento);
        }

        // GET: VistaVentasDepartamentoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaVentasDepartamentos == null)
            {
                return NotFound();
            }

            var vistaVentasDepartamento = await _context.VistaVentasDepartamentos.FindAsync(id);
            if (vistaVentasDepartamento == null)
            {
                return NotFound();
            }
            return View(vistaVentasDepartamento);
        }

        // POST: VistaVentasDepartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Venta")] VistaVentasDepartamento vistaVentasDepartamento)
        {
            if (id != vistaVentasDepartamento.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaVentasDepartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaVentasDepartamentoExists(vistaVentasDepartamento.Nombre))
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
            return View(vistaVentasDepartamento);
        }

        // GET: VistaVentasDepartamentoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaVentasDepartamentos == null)
            {
                return NotFound();
            }

            var vistaVentasDepartamento = await _context.VistaVentasDepartamentos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (vistaVentasDepartamento == null)
            {
                return NotFound();
            }

            return View(vistaVentasDepartamento);
        }

        // POST: VistaVentasDepartamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaVentasDepartamentos == null)
            {
                return Problem("Entity set 'CRMContext.VistaVentasDepartamentos'  is null.");
            }
            var vistaVentasDepartamento = await _context.VistaVentasDepartamentos.FindAsync(id);
            if (vistaVentasDepartamento != null)
            {
                _context.VistaVentasDepartamentos.Remove(vistaVentasDepartamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaVentasDepartamentoExists(string id)
        {
          return _context.VistaVentasDepartamentos.Any(e => e.Nombre == id);
        }
    }
}
