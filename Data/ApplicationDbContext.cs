﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Models;
using trabajo_final_grupo_verde.Models.Entity;

namespace trabajo_final_grupo_verde.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Contacto> Contactos { get; set; }


public DbSet<Producto> DataProducto {get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Proforma> DataItemCarrito { get; set; }
    public DbSet<Pago> DataPago {get; set; }
    public DbSet<Pedido> DataPedido {get; set; }
    public DbSet<DetallePedido> DataDetallePedido {get; set; }

}


