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
    }
}
