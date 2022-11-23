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
    public class TotalTareasYactividadesController : Controller
    {
        private readonly CRMContext _context;

        public TotalTareasYactividadesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TotalTareasYactividades
        public async Task<IActionResult> Index()
        {
              return View(await _context.TotalTareasYactividades.ToListAsync());
        }

        // GET: TotalTareasYactividades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TotalTareasYactividades == null)
            {
                return NotFound();
            }

            var totalTareasYactividade = await _context.TotalTareasYactividades
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (totalTareasYactividade == null)
            {
                return NotFound();
            }

            return View(totalTareasYactividade);
        }

        // GET: TotalTareasYactividades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TotalTareasYactividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroCotizacion,NombreOportunidad,TotalTareasYActividades")] TotalTareasYactividade totalTareasYactividade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(totalTareasYactividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(totalTareasYactividade);
        }

        // GET: TotalTareasYactividades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TotalTareasYactividades == null)
            {
                return NotFound();
            }

            var totalTareasYactividade = await _context.TotalTareasYactividades.FindAsync(id);
            if (totalTareasYactividade == null)
            {
                return NotFound();
            }
            return View(totalTareasYactividade);
        }

        // POST: TotalTareasYactividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroCotizacion,NombreOportunidad,TotalTareasYActividades")] TotalTareasYactividade totalTareasYactividade)
        {
            if (id != totalTareasYactividade.NumeroCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(totalTareasYactividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TotalTareasYactividadeExists(totalTareasYactividade.NumeroCotizacion))
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
            return View(totalTareasYactividade);
        }

        // GET: TotalTareasYactividades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TotalTareasYactividades == null)
            {
                return NotFound();
            }

            var totalTareasYactividade = await _context.TotalTareasYactividades
                .FirstOrDefaultAsync(m => m.NumeroCotizacion == id);
            if (totalTareasYactividade == null)
            {
                return NotFound();
            }

            return View(totalTareasYactividade);
        }

        // POST: TotalTareasYactividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TotalTareasYactividades == null)
            {
                return Problem("Entity set 'CRMContext.TotalTareasYactividades'  is null.");
            }
            var totalTareasYactividade = await _context.TotalTareasYactividades.FindAsync(id);
            if (totalTareasYactividade != null)
            {
                _context.TotalTareasYactividades.Remove(totalTareasYactividade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TotalTareasYactividadeExists(string id)
        {
          return _context.TotalTareasYactividades.Any(e => e.NumeroCotizacion == id);
        }
    }
}
