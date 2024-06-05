using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Models;
using trabajo_final_grupo_verde.Data;
using trabajo_final_grupo_verde.Models.Entity;

namespace trabajo_final_grupo_verde.Service
{
    public class PedidoService
    {
        private readonly ILogger<PedidoService> _logger;
        private readonly ApplicationDbContext _context;

        public PedidoService(ILogger<PedidoService> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Pedido>?> GetAll(){
            if(_context.DataPedido == null )
                return null;
            return await _context.DataPedido.ToListAsync();
        }
    }
}