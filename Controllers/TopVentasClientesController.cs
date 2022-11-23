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
    public class TopVentasClientesController : Controller
    {
        private readonly CRMContext _context;

        public TopVentasClientesController(CRMContext context)
        {
            _context = context;
        }

        // GET: TopVentasClientes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopVentasClientes.ToListAsync());
        }

        // GET: TopVentasClientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TopVentasClientes == null)
            {
                return NotFound();
            }

            var topVentasCliente = await _context.TopVentasClientes
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (topVentasCliente == null)
            {
                return NotFound();
            }

            return View(topVentasCliente);
        }

        // GET: TopVentasClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopVentasClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreCuenta,VentaTotal")] TopVentasCliente topVentasCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topVentasCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topVentasCliente);
        }

        // GET: TopVentasClientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TopVentasClientes == null)
            {
                return NotFound();
            }

            var topVentasCliente = await _context.TopVentasClientes.FindAsync(id);
            if (topVentasCliente == null)
            {
                return NotFound();
            }
            return View(topVentasCliente);
        }

        // POST: TopVentasClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreCuenta,VentaTotal")] TopVentasCliente topVentasCliente)
        {
            if (id != topVentasCliente.NombreCuenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topVentasCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopVentasClienteExists(topVentasCliente.NombreCuenta))
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
            return View(topVentasCliente);
        }

        // GET: TopVentasClientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TopVentasClientes == null)
            {
                return NotFound();
            }

            var topVentasCliente = await _context.TopVentasClientes
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (topVentasCliente == null)
            {
                return NotFound();
            }

            return View(topVentasCliente);
        }

        // POST: TopVentasClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TopVentasClientes == null)
            {
                return Problem("Entity set 'CRMContext.TopVentasClientes'  is null.");
            }
            var topVentasCliente = await _context.TopVentasClientes.FindAsync(id);
            if (topVentasCliente != null)
            {
                _context.TopVentasClientes.Remove(topVentasCliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopVentasClienteExists(string id)
        {
          return _context.TopVentasClientes.Any(e => e.NombreCuenta == id);
        }
    }
}
