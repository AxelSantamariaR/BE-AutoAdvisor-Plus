using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    [Table("nuevos")]
    public class Nuevo
    {
        [Key]
        public long Id_Nuevo { get; set; }

        [ForeignKey("Auto")]
        public long Id_Auto { get; set; }

        [Required]
        [StringLength(100)]
        public string? Marca { get; set; }

        [Required]
        [StringLength(100)]
        public string? Tipo { get; set; }

        [Required]
        [StringLength(100)]
        public string? Edicion { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public Auto? Auto { get; set; }
    }
}
