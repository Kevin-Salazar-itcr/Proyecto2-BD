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
    public class ClientesXzonasController : Controller
    {
        private readonly CRMContext _context;

        public ClientesXzonasController(CRMContext context)
        {
            _context = context;
        }

        // GET: ClientesXzonas
        public async Task<IActionResult> Index()
        {
              return View(await _context.ClientesXzonas.ToListAsync());
        }

        // GET: ClientesXzonas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ClientesXzonas == null)
            {
                return NotFound();
            }

            var clientesXzona = await _context.ClientesXzonas
                .FirstOrDefaultAsync(m => m.Zona == id);
            if (clientesXzona == null)
            {
                return NotFound();
            }

            return View(clientesXzona);
        }

        // GET: ClientesXzonas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientesXzonas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Zona,Cantidad,Monto")] ClientesXzona clientesXzona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientesXzona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientesXzona);
        }

        // GET: ClientesXzonas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ClientesXzonas == null)
            {
                return NotFound();
            }

            var clientesXzona = await _context.ClientesXzonas.FindAsync(id);
            if (clientesXzona == null)
            {
                return NotFound();
            }
            return View(clientesXzona);
        }

        // POST: ClientesXzonas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Zona,Cantidad,Monto")] ClientesXzona clientesXzona)
        {
            if (id != clientesXzona.Zona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientesXzona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesXzonaExists(clientesXzona.Zona))
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
            return View(clientesXzona);
        }

        // GET: ClientesXzonas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ClientesXzonas == null)
            {
                return NotFound();
            }

            var clientesXzona = await _context.ClientesXzonas
                .FirstOrDefaultAsync(m => m.Zona == id);
            if (clientesXzona == null)
            {
                return NotFound();
            }

            return View(clientesXzona);
        }

        // POST: ClientesXzonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ClientesXzonas == null)
            {
                return Problem("Entity set 'CRMContext.ClientesXzonas'  is null.");
            }
            var clientesXzona = await _context.ClientesXzonas.FindAsync(id);
            if (clientesXzona != null)
            {
                _context.ClientesXzonas.Remove(clientesXzona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesXzonaExists(string id)
        {
          return _context.ClientesXzonas.Any(e => e.Zona == id);
        }
    }
}
