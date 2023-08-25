using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("citas")]
    public class Cita
    {
        [Key]
        public int Id_Cita { get; set; }

        public long AsesorId_Asesor { get; set; }

        public long HorarioId_Horario { get; set; }

        public long AutoId_Auto { get; set; }

        [Required]
        [StringLength(255)]
        public string? NombresCliente { get; set; }

        [Required]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string? Correo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual Asesor? Asesor { get; set; }

        public virtual Horario? Horario { get; set; }

        public virtual Auto? Auto { get; set; }
    }
}
