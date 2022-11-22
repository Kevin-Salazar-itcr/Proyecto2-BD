using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Procesos;

namespace ProyectoCRM.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly crmContext _context;

        public ClientesController(crmContext context)
        {
            _context = context;
        }

        ClientesProcesos ClientesDatos = new ClientesProcesos();

        public async Task<IActionResult> Index()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = ClientesDatos.Listar();

            return View(oLista);

        }

     
      
        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre");
            ViewData["Idmoneda"] = new SelectList(_context.Moneda, "Id", "NombreMoneda");
            ViewData["Idsector"] = new SelectList(_context.Sectors, "Id", "Sector1");
            ViewData["Idzona"] = new SelectList(_context.Zonas, "Id", "Zona1");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Cliente cliente)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("agregarCliente", conexion);


                    cmd.Parameters.AddWithValue("@nombre_cuenta", cliente.NombreCuenta);
                    cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@sitio", cliente.Sitio);
                    cmd.Parameters.AddWithValue("@contactoP", cliente.ContactoPrincipal);
                    cmd.Parameters.AddWithValue("@asesor", User.Identity.Name.ToString());
                    cmd.Parameters.AddWithValue("@zona", cliente.Idzona);
                    cmd.Parameters.AddWithValue("@sector", cliente.Idsector);
                    cmd.Parameters.AddWithValue("@moneda", cliente.Idmoneda);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Cliente");


            }

                return RedirectToAction("Index", "Cliente");
            }



        


   
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.AsesorNavigation)
                .Include(c => c.IdmonedaNavigation)
                .Include(c => c.IdsectorNavigation)
                .Include(c => c.IdzonaNavigation)
                .FirstOrDefaultAsync(m => m.NombreCuenta == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

    

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("eliminarCliente", conexion);


                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Index", "Cliente");


            }

            return RedirectToAction("Index", "Cliente");

        }

    }
}
