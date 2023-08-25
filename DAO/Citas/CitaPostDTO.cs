using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaPostDTO
    {
        public long AsesorId_Asesor { get; set; }

        public long HorarioId_Horario { get; set; }

        public long AutoId_Auto { get; set; }

        public string? NombresCliente { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public DateTime Fecha { get; set; }

    }
}
