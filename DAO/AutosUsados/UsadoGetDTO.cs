using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AutosUsados
{
    public class UsadoGetDTO
    {
        public long Id_Auto { get; set; }

        public string? Nombre { get; set; }

        public int Anio { get; set; }

        public decimal Precio { get; set; }

        public string? Descripcion { get; set; }

        public string? Imagen { get; set; }

        public string? NombreVendedor { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Estado { get; set; }
    }
}
