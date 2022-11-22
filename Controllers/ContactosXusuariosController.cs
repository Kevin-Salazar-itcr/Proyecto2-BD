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
    public class ContactosXusuariosController : Controller
    {
        private readonly CRMContext _context;

        public ContactosXusuariosController(CRMContext context)
        {
            _context = context;
        }

        // GET: ContactosXusuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.ContactosXusuarios.ToListAsync());
        }

        // GET: ContactosXusuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ContactosXusuarios == null)
            {
                return NotFound();
            }

            var contactosXusuario = await _context.ContactosXusuarios
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (contactosXusuario == null)
            {
                return NotFound();
            }

            return View(contactosXusuario);
        }

        // GET: ContactosXusuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactosXusuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,CantidadDeContactos")] ContactosXusuario contactosXusuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactosXusuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactosXusuario);
        }

        // GET: ContactosXusuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ContactosXusuarios == null)
            {
                return NotFound();
            }

            var contactosXusuario = await _context.ContactosXusuarios.FindAsync(id);
            if (contactosXusuario == null)
            {
                return NotFound();
            }
            return View(contactosXusuario);
        }

        // POST: ContactosXusuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,CantidadDeContactos")] ContactosXusuario contactosXusuario)
        {
            if (id != contactosXusuario.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactosXusuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactosXusuarioExists(contactosXusuario.Nombre))
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
            return View(contactosXusuario);
        }

        // GET: ContactosXusuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ContactosXusuarios == null)
            {
                return NotFound();
            }

            var contactosXusuario = await _context.ContactosXusuarios
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (contactosXusuario == null)
            {
                return NotFound();
            }

            return View(contactosXusuario);
        }

        // POST: ContactosXusuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ContactosXusuarios == null)
            {
                return Problem("Entity set 'CRMContext.ContactosXusuarios'  is null.");
            }
            var contactosXusuario = await _context.ContactosXusuarios.FindAsync(id);
            if (contactosXusuario != null)
            {
                _context.ContactosXusuarios.Remove(contactosXusuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactosXusuarioExists(string id)
        {
          return _context.ContactosXusuarios.Any(e => e.Nombre == id);
        }
    }
}
