using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("horarios")]
    public class Horario
    {
        [Key]
        public long Id_Horario { get; set; }

        [Required]
        [StringLength(100)]
        public string? Hora { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    }
}
