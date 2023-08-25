using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AutoNuevo
{
    public class NuevoPostDTO
    {
        public string? Nombre { get; set; }

        public int Anio { get; set; }

        public decimal Precio { get; set; }

        public string? Descripcion { get; set; }

        public string? Imagen { get; set; }

        public string? Marca { get; set; }

        public string? Tipo { get; set; }

        public string? Edicion { get; set; }

    }
}
