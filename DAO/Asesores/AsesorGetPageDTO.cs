using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Asesor
{
    public class AsesorGetPageDTO
    {
        public string? Nombres { get; set; }

        public string? Descripcion { get; set; }

        public int AniosExperiencia { get; set; }

        public string? Especialidad { get; set; }

        public string? Idiomas { get; set; }

        public DateTime FechaNac { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Imagen { get; set; }

    }
}
