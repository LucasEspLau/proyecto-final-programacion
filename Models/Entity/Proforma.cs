using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace trabajo_final_grupo_verde.Models.Entity
{
    [Table("t_proforma")]
    public class Proforma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserID { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        [NotNull]
        public Decimal Precio { get; set; }
        public string Status { get; set; } ="PENDIENTE";
    }
}