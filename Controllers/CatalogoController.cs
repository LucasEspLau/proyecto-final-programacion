using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trabajo_final_grupo_verde.Data;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Models;
using Microsoft.AspNetCore.Identity;
using trabajo_final_grupo_verde.Models.Entity;

namespace trabajo_final_grupo_verde.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public CatalogoController(ILogger<CatalogoController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public IActionResult Index(string? searchString)
        {
            var productos = from o in _context.DataProducto select o;
            if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Nombre.Contains(searchString));
            }
            //productos = productos.Where(l => l.Status.Contains("A"));
            return View(productos.ToList());
        }
          public async Task<IActionResult> Detalle(int? id){
            Producto objProduct = await _context.DataProducto.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}