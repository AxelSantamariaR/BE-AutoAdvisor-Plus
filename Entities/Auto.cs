using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("autos")]
    public class Auto
    {
        [Key]
        public long Id_Auto { get; set; }

        [Required]
        [StringLength(255)]
        public string? Nombre { get; set; }

        [Required]        
        public int Anio { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(100)]
        public string? Imagen { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public Nuevo? Nuevo { get; set; }

        public Usado? Usado { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
    }
}
