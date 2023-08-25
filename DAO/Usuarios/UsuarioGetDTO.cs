using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Usuarios
{
    public class UsuarioGetDTO
    {
        public int Id { get; set; }

        public string? Cedula { get; set; }

        public string? Nombres { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Modulo { get; set; }

        public bool Estado { get; set; }

    }
}
