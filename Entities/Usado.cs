using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("usados")]
    public class Usado
    {
        [Key]
        public long Id_Usado { get; set; }

        [ForeignKey("Auto")]
        public long Id_Auto { get; set; }

        [Required]
        [StringLength(255)]
        public string? NombreVendedor { get; set; }

        [Required]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string? Correo { get; set; }

        public long EstadoUsadoId { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public Auto? Auto { get; set; }

        public virtual EstadoUsado EstadoUsado { get; set; } = null!;

    }
}
