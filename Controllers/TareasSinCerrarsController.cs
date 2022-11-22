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
    public class TareasSinCerrarsController : Controller
    {
        private readonly CRMContext _context;

        public TareasSinCerrarsController(CRMContext context)
        {
            _context = context;
        }

        // GET: TareasSinCerrars
        public async Task<IActionResult> Index()
        {
              return View(await _context.TareasSinCerrars.ToListAsync());
        }

        // GET: TareasSinCerrars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TareasSinCerrars == null)
            {
                return NotFound();
            }

            var tareasSinCerrar = await _context.TareasSinCerrars
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (tareasSinCerrar == null)
            {
                return NotFound();
            }

            return View(tareasSinCerrar);
        }

        // GET: TareasSinCerrars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TareasSinCerrars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaCreacion,Informacion,Nombre")] TareasSinCerrar tareasSinCerrar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tareasSinCerrar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tareasSinCerrar);
        }

        // GET: TareasSinCerrars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TareasSinCerrars == null)
            {
                return NotFound();
            }

            var tareasSinCerrar = await _context.TareasSinCerrars.FindAsync(id);
            if (tareasSinCerrar == null)
            {
                return NotFound();
            }
            return View(tareasSinCerrar);
        }

        // POST: TareasSinCerrars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FechaCreacion,Informacion,Nombre")] TareasSinCerrar tareasSinCerrar)
        {
            if (id != tareasSinCerrar.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tareasSinCerrar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareasSinCerrarExists(tareasSinCerrar.Nombre))
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
            return View(tareasSinCerrar);
        }

        // GET: TareasSinCerrars/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TareasSinCerrars == null)
            {
                return NotFound();
            }

            var tareasSinCerrar = await _context.TareasSinCerrars
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (tareasSinCerrar == null)
            {
                return NotFound();
            }

            return View(tareasSinCerrar);
        }

        // POST: TareasSinCerrars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TareasSinCerrars == null)
            {
                return Problem("Entity set 'CRMContext.TareasSinCerrars'  is null.");
            }
            var tareasSinCerrar = await _context.TareasSinCerrars.FindAsync(id);
            if (tareasSinCerrar != null)
            {
                _context.TareasSinCerrars.Remove(tareasSinCerrar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareasSinCerrarExists(string id)
        {
          return _context.TareasSinCerrars.Any(e => e.Nombre == id);
        }
    }
}
