using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace trabajo_final_grupo_verde.Models.Entity
{
    [Table("t_pago")]
    public class Pago
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? NombreTarjeta { get; set; }
        public string? NumeroTarjeta { get; set; }
        [NotMapped]
        public string? DueDateYYMM { get; set; }
        [NotMapped]
        public string? Cvv { get; set; }
        public Decimal MontoTotal{ get; set; }
        public string? UserID{ get; set; }
    }
}