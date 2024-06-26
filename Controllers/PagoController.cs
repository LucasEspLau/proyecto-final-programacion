using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trabajo_final_grupo_verde.Models;
using Microsoft.AspNetCore.Identity;
using trabajo_final_grupo_verde.Data;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Models.Entity;
using trabajo_final_grupo_verde.Service;

namespace trabajo_final_grupo_verde.Controllers
{
    public class PagoController : Controller
    {
        private readonly ILogger<PagoController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

          private readonly PagoService _pagoService;

        public PagoController(ILogger<PagoController> logger,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context, PagoService pagoService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _pagoService = pagoService;
        }

        public IActionResult Create(Decimal monto)
        {

            Pago pago = new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal = monto;
            return View(pago);
        }

        [HttpPost]
        public IActionResult Pagar(Pago pago)
        {
            pago.PaymentDate = DateTime.UtcNow;
            _context.Add(pago);

            var itemsCarrito = from o in _context.DataItemCarrito select o;
            itemsCarrito = itemsCarrito.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(pago.UserID) && s.Status.Equals("PENDIENTE"));

            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.Total = pago.MontoTotal;
            pedido.pago = pago;
            pedido.Status = "PENDIENTE";
            _context.Add(pedido);

            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach(var item in itemsCarrito.ToList()){
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.pedido=pedido;
                detallePedido.Precio = item.Precio;
                detallePedido.Producto = item.Producto;
                detallePedido.Cantidad = item.Cantidad;
                itemsPedido.Add(detallePedido);
            }


            _context.AddRange(itemsPedido);

            foreach (Proforma p in itemsCarrito.ToList())
            {
                p.Status="PROCESADO";
            }

            _context.UpdateRange(itemsCarrito);

            _context.SaveChanges();

            ViewData["Message"] = "El pago se ha registrado y su pedido nro "+ pedido.ID +" esta en camino";
            return View("Create");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

         public async Task<IActionResult> Index()
        {
         
            var pagos = await _pagoService.GetAll();
            return pagos != null ?
                        View(pagos) :
                        Problem("Entity set 'ApplicationDbContext.DataPago'  is null.");
        }

        
    }
}