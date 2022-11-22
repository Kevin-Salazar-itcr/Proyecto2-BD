using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
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
    public class ProductosCotizacionController : Controller
    {
        private readonly crmContext _context;

        public ProductosCotizacionController(crmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.ProductosXcotizacions.Include(p => p.CodigoProductoNavigation).Include(p => p.NumeroCotizacionNavigation);
            return View(await cRMContext.ToListAsync());
        }


        public IActionResult Create()
        {
            ViewData["CodigoProducto"] = new SelectList(_context.Productos, "Codigo", "Nombre");
            ViewData["NumeroCotizacion"] = new SelectList(_context.Cotizaciones, "NumeroCotizacion", "NumeroCotizacion");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductosXcotizacion productosXcotizacion)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("agregarProductos", conexion);


                    cmd.Parameters.AddWithValue("@codigo", productosXcotizacion.CodigoProducto);
                    cmd.Parameters.AddWithValue("@numeroCot", productosXcotizacion.NumeroCotizacion);
                    cmd.Parameters.AddWithValue("@cantidad", productosXcotizacion.Cantidad);
                    cmd.Parameters.AddWithValue("@precioNegociado", productosXcotizacion.PrecioNegociado);


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();





                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "ProductosCotizacion");

            }
            
            return RedirectToAction("index", "Cotizaciones");

        }
    }


}

