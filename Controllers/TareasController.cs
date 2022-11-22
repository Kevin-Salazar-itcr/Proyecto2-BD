using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCRM.Models;
using ProyectoCRM.Models.ViewModels;
using ProyectoCRM.Controllers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoCRM.Controllers
{

    [Authorize]
    public class TareasController : Controller
    {


        private readonly crmContext _context;

        public TareasController(crmContext context)
        {
            _context = context;
        }

   
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Tareas.Include(t => t.AsesorNavigation).Include(t => t.EstadoNavigation);
            return View(await cRMContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.AsesorNavigation)
                .Include(t => t.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }


        public async Task<IActionResult> Create(short? id)
        {

            Globales.contacto = (short)id;


            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula");
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Estado1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("agregarTarea", conexion);


                    cmd.Parameters.AddWithValue("@idContacto", Globales.contacto);
                    cmd.Parameters.AddWithValue("@id", tarea.Id);
                    cmd.Parameters.AddWithValue("@estado", tarea.Estado);
                    cmd.Parameters.AddWithValue("@fechaFinalizacion", tarea.FechaFinalizacion);
                    cmd.Parameters.AddWithValue("@informacion", tarea.Informacion);
                    cmd.Parameters.AddWithValue("@fechaCreacion", tarea.FechaCreacion);
                    cmd.Parameters.AddWithValue("@asesor", tarea.Asesor);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Tareas");

            }




         return RedirectToAction("index", "Contacto");

        }

    
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", tarea.Asesor);
            ViewData["Estado"] = new SelectList(_context.Estados, "Id", "Estado1", tarea.Estado);
            return View(tarea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, Tarea tarea)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("editarTarea", conexion);


                    cmd.Parameters.AddWithValue("@id", tarea.Id);
                    cmd.Parameters.AddWithValue("@estado", tarea.Estado);
                    cmd.Parameters.AddWithValue("@fechafin", tarea.FechaFinalizacion);
                    cmd.Parameters.AddWithValue("@asesor", tarea.Asesor);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Edit", "Tareas");

            }
        return RedirectToAction("index", "Tareas");
        }
    }
}
