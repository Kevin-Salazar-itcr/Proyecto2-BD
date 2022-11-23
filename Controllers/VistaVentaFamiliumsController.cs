
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
    public class VistaVentaFamiliumsController : Controller
    {
        private readonly CRMContext _context;

        public VistaVentaFamiliumsController(CRMContext context)
        {
            _context = context;
        }

        // GET: VistaVentaFamiliums
        public async Task<IActionResult> Index()
        {
              return View(await _context.VistaVentaFamilia.ToListAsync());
        }

        // GET: VistaVentaFamiliums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VistaVentaFamilia == null)
            {
                return NotFound();
            }

            var vistaVentaFamilium = await _context.VistaVentaFamilia
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (vistaVentaFamilium == null)
            {
                return NotFound();
            }

            return View(vistaVentaFamilium);
        }

        // GET: VistaVentaFamiliums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistaVentaFamiliums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Venta")] VistaVentaFamilium vistaVentaFamilium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistaVentaFamilium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistaVentaFamilium);
        }

        // GET: VistaVentaFamiliums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VistaVentaFamilia == null)
            {
                return NotFound();
            }

            var vistaVentaFamilium = await _context.VistaVentaFamilia.FindAsync(id);
            if (vistaVentaFamilium == null)
            {
                return NotFound();
            }
            return View(vistaVentaFamilium);
        }

        // POST: VistaVentaFamiliums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Venta")] VistaVentaFamilium vistaVentaFamilium)
        {
            if (id != vistaVentaFamilium.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistaVentaFamilium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistaVentaFamiliumExists(vistaVentaFamilium.Nombre))
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
            return View(vistaVentaFamilium);
        }

        // GET: VistaVentaFamiliums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VistaVentaFamilia == null)
            {
                return NotFound();
            }

            var vistaVentaFamilium = await _context.VistaVentaFamilia
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (vistaVentaFamilium == null)
            {
                return NotFound();
            }

            return View(vistaVentaFamilium);
        }

        // POST: VistaVentaFamiliums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VistaVentaFamilia == null)
            {
                return Problem("Entity set 'CRMContext.VistaVentaFamilia'  is null.");
            }
            var vistaVentaFamilium = await _context.VistaVentaFamilia.FindAsync(id);
            if (vistaVentaFamilium != null)
            {
                _context.VistaVentaFamilia.Remove(vistaVentaFamilium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistaVentaFamiliumExists(string id)
        {
          return _context.VistaVentaFamilia.Any(e => e.Nombre == id);
        }
    }
}
