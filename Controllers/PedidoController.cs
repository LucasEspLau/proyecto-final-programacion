using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Data;
using trabajo_final_grupo_verde.Service;


namespace trabajo_final_grupo_verde.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly PedidoService _pedidoService;

        public PedidoController(ILogger<PedidoController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context, PedidoService pedidoService )
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _pedidoService = pedidoService;
        }

        public  async Task<IActionResult>  Index()
        {
             var pedidos = from o in _context.DataPedido select o;
             pedidos = pedidos.Where(s => s.Status.Contains("PENDIENTE"));
            
              var pedido = await _pedidoService.GetAll();
            return pedido != null ?
                        View(pedido) :
                        Problem("Entity set 'ApplicationDbContext.DataPedidos'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}