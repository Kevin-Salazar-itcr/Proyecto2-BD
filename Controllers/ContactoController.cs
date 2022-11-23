using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using ProyectoCRM.Procesos;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoCRM.Controllers
{
    [Authorize]
    public class ContactoController : Controller
    {
        private readonly crmContext _context;

        public ContactoController(crmContext context)
        {
            _context = context;
        }



        contactosProcesos ContactosDatos = new contactosProcesos();




        public async Task<IActionResult> Index1(string buscar)
        {



            if (!String.IsNullOrEmpty(buscar))
            {
                var oLista1 = ContactosDatos.ListarClient(buscar);
                return View(oLista1);
            }

            var oLista = ContactosDatos.ListarTodo();

            return View(oLista);
        }



        public async Task<IActionResult> Index2()
        {

            return View();
        }


        public async Task<IActionResult> Index()
        {

                    
            var viewModel = new ContactoIndex();
            viewModel.Contactos = await _context.Contactos

                  .Include(i => i.Tareas)
                  .Include(i => i.Actividads)
                  .Include(i => i.ZonaNavigation)
                  .Include(i => i.SectorNavigation)
                  .Include(i => i.AsesorNavigation)
                  .Include(i => i.TipoContactoNavigation)
                  .Include(i => i.EstadoNavigation)
                  .AsNoTracking()
                  .ToListAsync();

            return View(viewModel);
        }

        contactosProcesos ContactoDatos = new contactosProcesos();


        public async Task<IActionResult> Details(short? id)
        {

            var oLista = ContactoDatos.Listar((short)id);

            return View(oLista);
        }


        public IActionResult Create()
        {
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre");
            ViewData["Cliente"] = new SelectList(_context.Clientes, "NombreCuenta", "NombreCuenta");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Estado1");
            ViewData["Sector"] = new SelectList(_context.Sectors, "Id", "Sector1");
            ViewData["TipoContacto"] = new SelectList(_context.TipoContactos, "Id", "Tipo");
            ViewData["Zona"] = new SelectList(_context.Zonas, "Id", "Zona1");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto contacto)
        {


            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("agregarContacto", conexion);


                    cmd.Parameters.AddWithValue("@idContacto", contacto.IdContacto);
                    cmd.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                    cmd.Parameters.AddWithValue("@motivo", contacto.Motivo);
                    cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
                    cmd.Parameters.AddWithValue("@correo", contacto.Correo);
                    cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
                    cmd.Parameters.AddWithValue("@descripcion", contacto.Descripcion);
                    cmd.Parameters.AddWithValue("@cliente", contacto.Cliente);
                    cmd.Parameters.AddWithValue("@zona", contacto.Zona);
                    cmd.Parameters.AddWithValue("@sector", contacto.Sector);
                    cmd.Parameters.AddWithValue("@asesor", contacto.Asesor);
                    cmd.Parameters.AddWithValue("@tipoContacto", contacto.TipoContacto);
                    cmd.Parameters.AddWithValue("@estado", contacto.Estado);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Contacto");


            }

                return RedirectToAction("index", "Contacto");


            }
        }
       
    }

