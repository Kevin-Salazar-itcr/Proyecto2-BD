﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;

namespace ProyectoCRM.Controllers
{

    [Authorize] 
    public class ProductoController : Controller
    {
        private readonly CRMContext _context;

        public ProductoController(CRMContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Productos.Include(p => p.ObjCodigoFamilia);
            return View(await cRMContext.ToListAsync());
        }




        [HttpGet]
        public IActionResult producto_detalle()

        {

            ProductoVM OproductoVM = new ProductoVM()
            {

                ObjProducto = new Producto(),
                ObjListaFamilia = _context.FamiliaProductos.Select(familia => new SelectListItem()
                {

                    Text = familia.Nombre,
                    Value = familia.Codigo.ToString()

                }).ToList()


            };

            return View(OproductoVM);

        }
        
      


        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodigoFamilia"] = new SelectList(_context.FamiliaProductos, "Codigo", "Codigo", producto.CodigoFamilia);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Nombre,Descripcion,Precio,Activo,CodigoFamilia")] Producto producto)
        {
            if (id != producto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Codigo))
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
            ViewData["CodigoFamilia"] = new SelectList(_context.FamiliaProductos, "Codigo", "Codigo", producto.CodigoFamilia);
            return View(producto);
        }

        private bool ProductoExists(string id)
        {
          return _context.Productos.Any(e => e.Codigo == id);
        }
    }
}