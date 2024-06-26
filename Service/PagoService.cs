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
    public class PagoService
    {
        private readonly ILogger<PagoService> _logger;
        private readonly ApplicationDbContext _context;

        public PagoService(ILogger<PagoService> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Pago>?> GetAll(){
            if(_context.DataPago == null )
                return null;
            return await _context.DataPago.ToListAsync();
        }
    }
}