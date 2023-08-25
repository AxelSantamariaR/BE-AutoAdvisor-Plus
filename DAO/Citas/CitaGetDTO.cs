using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Citas
{
    public class CitaGetDTO
    {
        public int Id_Cita { get; set; }

        public string? Auto { get; set; }

        public string? NombresCliente { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Hora { get; set; }

        public DateTime Fecha { get; set; }

        public string? NombresAsesor { get; set; }

        public bool Estado { get; set; }

    }
}
