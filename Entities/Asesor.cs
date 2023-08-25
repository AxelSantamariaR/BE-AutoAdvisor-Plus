using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains
{
    [Table("asesores")]
    public class Asesor
    {
        [Key]       
        public long Id_Asesor { get; set; }

        [Required]
        [StringLength(255)]        
        public string? Nombres { get; set; }

        [Required]
        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public int AniosExperiencia { get; set; }

        [Required]
        [StringLength(255)]
        public string? Especialidad { get; set; }

        [Required]
        [StringLength(255)]
        public string? Idiomas { get; set; }

        [Required]
        public DateTime FechaNac { get; set; }

        [Required]
        [StringLength(100)]
        public string? Correo { get; set; }

        [Required]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string? Imagen { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaReg { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();


    }
}