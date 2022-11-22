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
    public class ConsultaCierreEjecucionesController : Controller
    {
        private readonly CRMContext _context;

        public ConsultaCierreEjecucionesController(CRMContext context)
        {
            _context = context;
        }

        // GET: ConsultaCierreEjecuciones
        public async Task<IActionResult> Index()
        {
              return View(await _context.ConsultaCierreEjecuciones.ToListAsync());
        }

        // GET: ConsultaCierreEjecuciones/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.ConsultaCierreEjecuciones == null)
            {
                return NotFound();
            }

            var consultaCierreEjecucione = await _context.ConsultaCierreEjecuciones
                .FirstOrDefaultAsync(m => m.Idejecucion == id);
            if (consultaCierreEjecucione == null)
            {
                return NotFound();
            }

            return View(consultaCierreEjecucione);
        }

        // GET: ConsultaCierreEjecuciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsultaCierreEjecuciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idejecucion,Nombre,NumeroCotizacion")] ConsultaCierreEjecucione consultaCierreEjecucione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultaCierreEjecucione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultaCierreEjecucione);
        }

        // GET: ConsultaCierreEjecuciones/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.ConsultaCierreEjecuciones == null)
            {
                return NotFound();
            }

            var consultaCierreEjecucione = await _context.ConsultaCierreEjecuciones.FindAsync(id);
            if (consultaCierreEjecucione == null)
            {
                return NotFound();
            }
            return View(consultaCierreEjecucione);
        }

        // POST: ConsultaCierreEjecuciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Idejecucion,Nombre,NumeroCotizacion")] ConsultaCierreEjecucione consultaCierreEjecucione)
        {
            if (id != consultaCierreEjecucione.Idejecucion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultaCierreEjecucione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaCierreEjecucioneExists(consultaCierreEjecucione.Idejecucion))
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
            return View(consultaCierreEjecucione);
        }

        // GET: ConsultaCierreEjecuciones/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.ConsultaCierreEjecuciones == null)
            {
                return NotFound();
            }

            var consultaCierreEjecucione = await _context.ConsultaCierreEjecuciones
                .FirstOrDefaultAsync(m => m.Idejecucion == id);
            if (consultaCierreEjecucione == null)
            {
                return NotFound();
            }

            return View(consultaCierreEjecucione);
        }

        // POST: ConsultaCierreEjecuciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.ConsultaCierreEjecuciones == null)
            {
                return Problem("Entity set 'CRMContext.ConsultaCierreEjecuciones'  is null.");
            }
            var consultaCierreEjecucione = await _context.ConsultaCierreEjecuciones.FindAsync(id);
            if (consultaCierreEjecucione != null)
            {
                _context.ConsultaCierreEjecuciones.Remove(consultaCierreEjecucione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaCierreEjecucioneExists(short id)
        {
          return _context.ConsultaCierreEjecuciones.Any(e => e.Idejecucion == id);
        }
    }
}
