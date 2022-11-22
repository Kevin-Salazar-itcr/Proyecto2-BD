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
    public class ActividadController : Controller
    {
        private readonly crmContext _context;

        public ActividadController(crmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Actividads.Include(a => a.AsesorNavigation);
            return View(await cRMContext.ToListAsync());
        }


        public IActionResult Create(short? id)
        {

            Globales.contacto = (short)id;
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actividad actividad)

        {

            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("agregarActividad", conexion);


                    cmd.Parameters.AddWithValue("@idContacto", Globales.contacto);
                    cmd.Parameters.AddWithValue("@id", actividad.Id);
                    cmd.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                    cmd.Parameters.AddWithValue("@fechaIni", actividad.FechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", actividad.FechaFin);
                    cmd.Parameters.AddWithValue("@asesor", actividad.Asesor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();




                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Actividad");


            }
            return RedirectToAction("index", "Contacto");

        }
    



        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Actividads == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividads.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            ViewData["Asesor"] = new SelectList(_context.Usuarios, "Cedula", "Cedula", actividad.Asesor);
            return View(actividad);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id,  Actividad actividad)
        {

            try
            {
                using (SqlConnection conexion = new SqlConnection("Data Source=localhost ; Initial Catalog=CRM; Integrated Security=true"))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("editarActividad", conexion);


                    cmd.Parameters.AddWithValue("@id", actividad.Id);
                    cmd.Parameters.AddWithValue("@fechaFin", actividad.FechaFin);
                    cmd.Parameters.AddWithValue("@asesor", actividad.Asesor);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    return RedirectToAction("index", "Actividad");



                }
            }
            catch (Exception e)
            {

                string error = e.Message;
                return RedirectToAction("Create", "Actividad");


            }

            return RedirectToAction("index", "Actividad");


            }
        }

    }

