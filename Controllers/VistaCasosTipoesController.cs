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
    public class VistaCasosTipoesController : Controller
    {
        private readonly CRMContext _context;

        public VistaCasosTipoesController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaCasosTipoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaCasosTipos.ToListAsync());
        }

        // GET: VistaCasosTipoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaCasosTipos == null)
            {
                return NotFound();
            }

            var vistaCasosTipo = await _context.VistaCasosTipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (vistaCasosTipo == null)
            {
                return NotFound();
            }

            return View(vistaCasosTipo);
        }

        // GET: VistaCasosTipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaCasosTipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Casos")] VistaCasosTipo vistaCasosTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaCasosTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaCasosTipo);
        }

        // GET: VistaCasosTipoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaCasosTipos == null)
            {
                return NotFound();
            }

            var vistaCasosTipo = await _context.VistaCasosTipos.FindAsync(id);
            if (vistaCasosTipo == null)
            {
                return NotFound();
            }
            return View(vistaCasosTipo);
        }

        // POST: VistaCasosTipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Tipo,Casos")] VistaCasosTipo vistaCasosTipo)
        {
            if (id != vistaCasosTipo.Tipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaCasosTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaCasosTipoExists(vistaCasosTipo.Tipo))
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
            return View(vistaCasosTipo);
        }

        // GET: VistaCasosTipoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaCasosTipos == null)
            {
                return NotFound();
            }

            var vistaCasosTipo = await _context.VistaCasosTipos
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (vistaCasosTipo == null)
            {
                return NotFound();
            }

            return View(vistaCasosTipo);
        }

        // POST: VistaCasosTipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaCasosTipos == null)
            {
                return Problem("Entity set 'CRMContext.VistaCasosTipos'  is null.");
            }
            var vistaCasosTipo = await _context.VistaCasosTipos.FindAsync(id);
            if (vistaCasosTipo != null)
            {
                _context.VistaCasosTipos.Remove(vistaCasosTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaCasosTipoExists(string id)
        {
          return _context.VistaCasosTipos.Any(e => e.Tipo == id);
        }
    }
}
