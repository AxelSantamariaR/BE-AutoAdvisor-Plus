using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domains
{
    [Table("Usuarios")]
    [Index(nameof(Cedula))]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string? Cedula { get; set; }

        [Required]
        [StringLength(255)]
        public string? Nombres { get; set; }

        [Required]
        [StringLength(255)]
        public string? Username { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        [Required]
        public int? RolId { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual Rol? Roles { get; set; }

    }
}
