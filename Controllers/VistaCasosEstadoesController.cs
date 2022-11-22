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
    public class VistaCasosEstadoesController : Controller
    {
        private readonly CRMContext _context;

        public VistaCasosEstadoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaCasosEstadoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaCasosEstados.ToListAsync());
        }

        // GET: VistaCasosEstadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VistaCasosEstados == null)
            {
                return NotFound();
            }

            var vistaCasosEstado = await _context.VistaCasosEstados
                .FirstOrDefaultAsync(m => m.Casos == id);
            if (vistaCasosEstado == null)
            {
                return NotFound();
            }

            return View(vistaCasosEstado);
        }

        // GET: VistaCasosEstadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaCasosEstadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado,Casos")] VistaCasosEstado vistaCasosEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaCasosEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaCasosEstado);
        }

        // GET: VistaCasosEstadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VistaCasosEstados == null)
            {
                return NotFound();
            }

            var vistaCasosEstado = await _context.VistaCasosEstados.FindAsync(id);
            if (vistaCasosEstado == null)
            {
                return NotFound();
            }
            return View(vistaCasosEstado);
        }

        // POST: VistaCasosEstadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Estado,Casos")] VistaCasosEstado vistaCasosEstado)
        {
            if (id != vistaCasosEstado.Casos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaCasosEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaCasosEstadoExists(vistaCasosEstado.Casos))
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
            return View(vistaCasosEstado);
        }

        // GET: VistaCasosEstadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VistaCasosEstados == null)
            {
                return NotFound();
            }

            var vistaCasosEstado = await _context.VistaCasosEstados
                .FirstOrDefaultAsync(m => m.Casos == id);
            if (vistaCasosEstado == null)
            {
                return NotFound();
            }

            return View(vistaCasosEstado);
        }

        // POST: VistaCasosEstadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.VistaCasosEstados == null)
            {
                return Problem("Entity set 'CRMContext.VistaCasosEstados'  is null.");
            }
            var vistaCasosEstado = await _context.VistaCasosEstados.FindAsync(id);
            if (vistaCasosEstado != null)
            {
                _context.VistaCasosEstados.Remove(vistaCasosEstado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaCasosEstadoExists(int? id)
        {
          return _context.VistaCasosEstados.Any(e => e.Casos == id);
        }
    }
}
