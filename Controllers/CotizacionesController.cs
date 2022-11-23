using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.logica;
using ProyectoCRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ProyectoCRM.Procesos;

namespace ProyectoCRM.Controllers
{
    [Authorize]
    public class CotizacionesController : Controller
    {
        private readonly crmContext _context;

        public CotizacionesController(crmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index1()
        {

            return View();

        }

        public async Task<IActionResult> Index()
        {

            var viewModel = new cotizacionesIndex();
            viewModel.Cotizaciones = await _context.Cotizaciones

                  .Include(i => i.ContactoAsociadoNavigation)
                  .Include(i => i.AsesorNavigation)
                  .Include(i => i.EtapaNavigation)
                  .Include(i => i.AsesorNavigation)
                  .Include(i => i.NombreCuentaNavigation)
                  .Include(i => i.TipoNavigation)
                  .Include(i => i.ProductosXcotizacions)
                 
                 
                  



                  .AsNoTracking()
                  .ToListAsync();

            return View(viewModel);

        }


        CotizacionesProcesos cotDatos = new CotizacionesProcesos();


        public async Task<IActionResult> Details(string id)
        {

            var oLista = cotDatos.Listar(id);

            return View(oLista);
        }


        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre");
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "Nombre");
            ViewData["ContraQuien"] = new SelectList(_context.Rivales, "Id", "Nombre");
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Id", "Etapa1");
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Probabilidad"] = new SelectList(_context.Probabilidads, "Id", "Etapa");
            ViewData["Moneda"] = new SelectList(_context.Moneda, "Id", "NombreMoneda");
            ViewData["RazonDenegacion"] = new SelectList(_context.CotizacionDenegada, "Id", "Razon");
            ViewData["Tipo"] = new SelectList(_context.TipoCotizacions, "Id", "Tipo");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cotizacione cotizacione)
        {

            //UtiliZamos la funcion de encontrar contacto
            Contacto objeto = new log().EncontrarContacto(cotizacione.ContactoAsociado);


            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("agregarCotizacion", conexion);


                    cmd.Parameters.AddWithValue("@numeroCot", cotizacione.NumeroCotizacion);
                    cmd.Parameters.AddWithValue("@nombreOpor", cotizacione.NombreOportunidad);
                    cmd.Parameters.AddWithValue("@fechaCot", cotizacione.FechaCotizacion);
                    cmd.Parameters.AddWithValue("@fechaCierre", cotizacione.FechaCierra);
                    cmd.Parameters.AddWithValue("@ordenCompra", cotizacione.OrdenCompra);
                    cmd.Parameters.AddWithValue("@descripcion", cotizacione.Descripcion);
                    cmd.Parameters.AddWithValue("@factura", cotizacione.Factur);
                    cmd.Parameters.AddWithValue("@zona", objeto.Zona);
                    cmd.Parameters.AddWithValue("@sector", objeto.Sector);
                    cmd.Parameters.AddWithValue("@moneda", cotizacione.Moneda);
                    cmd.Parameters.AddWithValue("@contactoAsociado", cotizacione.ContactoAsociado);
                    cmd.Parameters.AddWithValue("@asesor", objeto.Asesor);
                    cmd.Parameters.AddWithValue("@nombreCuenta", objeto.Cliente);
                    cmd.Parameters.AddWithValue("@etapa", cotizacione.Etapa);
                    cmd.Parameters.AddWithValue("@probabilidad", cotizacione.Probabilidad);
                    cmd.Parameters.AddWithValue("@tipo", cotizacione.Tipo);
                    cmd.Parameters.AddWithValue("@razon", cotizacione.RazonDenegacion);
                    cmd.Parameters.AddWithValue("@contraQuien", cotizacione.ContraQuien);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                  


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Cotizaciones");


            }


         return RedirectToAction("index", "Cotizaciones");


            
        }



        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones.FindAsync(id);
            if (cotizacione == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre", cotizacione.Asesor);
            ViewData["ContactoAsociado"] = new SelectList(_context.Contactos, "IdContacto", "Nombre", cotizacione.ContactoAsociado);
            ViewData["ContraQuien"] = new SelectList(_context.Rivales, "Id", "Nombre", cotizacione.ContraQuien);
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Id", "Etapa1", cotizacione.Etapa);
            ViewData["Moneda"] = new SelectList(_context.Moneda, "Id", "NombreMoneda", cotizacione.Moneda);
            ViewData["NombreCuenta"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta", cotizacione.NombreCuenta);
            ViewData["Probabilidad"] = new SelectList(_context.Probabilidads, "Id", "Etapa", cotizacione.Probabilidad);
            ViewData["RazonDenegacion"] = new SelectList(_context.CotizacionDenegada, "Id", "Razon", cotizacione.RazonDenegacion);
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Sector1", cotizacione.Sector);
            ViewData["Tipo"] = new SelectList(_context.TipoCotizacions, "Id", "Tipo", cotizacione.Tipo);
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Zona1", cotizacione.Zona);
            return View(cotizacione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Cotizacione cotizacione)
        {


            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("editarCotizacion", conexion);


                    cmd.Parameters.AddWithValue("@numeroCot", cotizacione.NumeroCotizacion);
                    cmd.Parameters.AddWithValue("@nombreOportunidad", cotizacione.NombreOportunidad);
                    cmd.Parameters.AddWithValue("@fechaCotizacion", cotizacione.FechaCotizacion);
                    cmd.Parameters.AddWithValue("@fechaCierre", cotizacione.FechaCierra);
                    cmd.Parameters.AddWithValue("@ordenCompra", cotizacione.OrdenCompra);
                    cmd.Parameters.AddWithValue("@descripcion", cotizacione.Descripcion);
                    cmd.Parameters.AddWithValue("@factura", cotizacione.Factur);
                    cmd.Parameters.AddWithValue("@moneda", cotizacione.Moneda);
                    cmd.Parameters.AddWithValue("@etapa", cotizacione.Etapa);
                    cmd.Parameters.AddWithValue("@probabilidad", cotizacione.Probabilidad);
                    cmd.Parameters.AddWithValue("@tipo", cotizacione.Tipo);
                    cmd.Parameters.AddWithValue("@razon", cotizacione.RazonDenegacion);
                    cmd.Parameters.AddWithValue("@contraQuien", cotizacione.ContraQuien);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();



                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Edit", "Cotizaciones");


            }


         return RedirectToAction("index", "Cotizaciones");


            
        }
    }
}

