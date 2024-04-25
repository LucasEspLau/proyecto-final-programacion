using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trabajo_final_grupo_verde.Models;

namespace trabajo_final_grupo_verde.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;

        public CarritoController(ILogger<CarritoController> logger)
        {
            _logger = logger;
        }
        public class CarritoViewModel
        {
            public List<Producto> Productos { get; set; }
            public decimal TotalCosto { get; set; }
        }


        public IActionResult Index()
        {   
            List<Producto> productos = new List<Producto>();
            foreach (var key in HttpContext.Session.Keys)
            {   
                if (key.StartsWith("Producto"))
                {
                    var producto = Util.SessionExtensions.Get<Producto>(HttpContext.Session, key);
                    if (producto != null)
                    {
                        productos.Add(producto);
                    }
                }
            }
            decimal totalCosto = productos.Sum(p => p.Precio);


                var viewModel = new CarritoViewModel
                {
                    Productos = productos,
                    TotalCosto = totalCosto
                };

                return View("ListaCarrito", viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}