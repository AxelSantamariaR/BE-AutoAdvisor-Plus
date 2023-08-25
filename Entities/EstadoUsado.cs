using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("estadoUsado")]
    public class EstadoUsado
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual ICollection<Usado> Usados { get; set; } = new List<Usado>();

    }
}
