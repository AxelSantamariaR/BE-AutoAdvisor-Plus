using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Modulo { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
